start_time = Time.now
class BusinessManager
  attr_accessor :name, :workYears, :averageProfit, :profit

  def initialize(name, workYears, averageProfit)
    @name = name
    @workYears = workYears
    @averageProfit = averageProfit
    @profit = 0
  end

  def to_s
    format('| %-20s | %10d | %10.2f | %10.2f |', name, workYears, averageProfit, profit)
  end
end

workersCount = 4

# Resultatų saugotojas
resultKeeper = Ractor.new do
  result = []
  loop do
    manager = Ractor.receive
    break if manager == :stop
    # Randamas indeksas kur reikia įterpti į sąrašą pagal pelną naują vadybininką (didėjimo tvarka)
    index = result.index { |x| x.profit > manager.profit }

    if index.nil?
      result.push(manager)
    else
      result.insert(index, manager)
    end
  end
  Ractor.yield result
end

puts 'Result keeper ready'
# Spausdintuvas
printer = Ractor.new do
  File.open('result.txt', 'w') do |file|
    results = Ractor.receive
    results.each { |value| file.puts(value.to_s) }
  end
  Ractor.yield :stop
end

puts 'Printer ready'
# Skirtytuvas
splitter = Ractor.new(resultKeeper, printer, workersCount) do |resultKeeper, printer, workersCount|
  workersDone = 0
  loop do
    # Darbuotojams baigus darbą, skirstytuvas taip pat baigia
    break if workersDone == workersCount

    manager = Ractor.receive
    # Darbuotojo darbo baigimo žinutė
    if manager == :workerFinished
      workersDone += 1
      next
    end
    # Pagrindinio scenarijaus ženklas, kad naujų duomenų nebebus
    if manager == :stop
      Ractor.yield :stop
      next
    end
    # Tikrinamas vadybininkas jeigu pelnas 0, tai reikia siųsti darbuotojui. Apskaičiuoti pelną.
    # Jeigu pelnas ne 0, tai reikia perduoti duomenų saugotojui.
    if manager.profit.zero?
      Ractor.yield manager
    elsif manager.profit.positive?
      resultKeeper.send manager
    end
  end
  # Baigus darbą, skirstytuvas perduoda duomenų saugotojui, kad naujų duomenų nebebus ir paima iš jo rezultatus
  # Kuriuos perduoda spausdintuvui
  resultKeeper.send :stop
  printer.send resultKeeper.take
end

puts 'Splitter ready'
# Darbuotojų kūrimas
workers = workersCount.times.map do # Worker actor
  Ractor.new(splitter) do |splitter|
    loop do
      manager = splitter.take
      # Darbo pabaigos žinutės tikrinimas
      break if manager == :stop
      # Sumos skaičiavimas (ilgai užtrunkanti funkcija)
      sum = 0
      (0..manager.workYears**3).each do
        (0..manager.workYears**3).each do
          (0..manager.workYears).each do
            sum += manager.averageProfit
          end
        end
      end

      manager.profit = sum
      # Kriterijaus tikrinimas
      splitter.send manager if manager.profit > 10000000
    end
    splitter.send :workerFinished
  end
end

puts 'Workers ready'

managers = []
# Duomenų nuskaitymas iš failo
File.foreach('dat_1.txt') do |line|
  values = line.split ' '
  manager = BusinessManager.new(values[0], values[1].to_i, values[2].to_f)
  managers.push(manager)
end

# Duomenų siuntimas į skirstytuvą
managers.each { |manager| splitter.send manager }
# Skirstytuvui paduodamos žinutės, kad darbuotojai gali baigti darbą
(0..workersCount-1).each { splitter.send :stop }
# Pagrindinis scenarijus turi laukti kol spausdintuvas baigs darbą.
# Jeigu nebus laukimo pagrindinis scenarijus išjungs visus aktorius ir baigs darbą.
waitForPrinterToFinish = printer.take
endTime = Time.now
puts "Time elapsed #{endTime - start_time} seconds with #{workersCount} workers"

# Testavimas atliktas:
# CPU
# 	Intel(R) Core(TM) i7-9750H CPU @ 2.60GHz
# 	Bazinis greitis:	2.59 GHz
# 	Branduoliai:	6
# 	Loginiai procesoriai:	12

#Darbuotojų sk. | Laikas
# 1             | 54.8439885 s
# 2             | 34.4577009 s
# 3             | 28.1122288 s
# 4             | 26.637133 s
# 5             | 23.6874267 s
# 6             | 23.1256484 s