using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KompiuterinisZaidimas
{
    class Program
    {
        static void Main(string[] args)
        {
            //lietuvišku rašmenų palaikymas
            Console.InputEncoding = Encoding.GetEncoding(1257);
            Console.OutputEncoding = Encoding.GetEncoding(1257);

            //Herojai.csv - visi 10 su geriausiomis savybėmis, daugiausiai herojų iš elfu ir orkų klasių (po 4),
            //elfai spausdinami i faila Elfai.csv
            //Herojai2.csv - vienas geriausiomis savybėmis ir tik viena geriausia klasė, elfu nėra.

            List<Hero> Heroes = InOut.ReadHeroes(@"Herojai.csv");

            if (Heroes.Capacity != 0)
            {
                List<Hero> BestHeroes = Task.BestCharacteristics(Heroes);        
                List<Races> Races = Task.SortRaces(Heroes);

                InOut.PrintBestHeroes(BestHeroes);

                InOut.PrintLargestGroups(Task.BiggestGroup(Races), Races, Heroes);

                InOut.PrintElves(Heroes);

            }
            else Console.WriteLine("Herojų failas tuščias arba nėra failo");


            // Console.ReadKey();
        }
    }
}
