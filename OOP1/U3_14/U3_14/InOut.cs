using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace U3_14
{
    class InOut
    {
        /// <summary>
        /// Nuskaitomi automobiliai iš failo.
        /// </summary>
        /// <param name="fileName">Failo pavadinimas</param>
        public static Register ReadAutos(string fileName)
        {
            Register register;
            string[] Lines = File.ReadAllLines(fileName, Encoding.GetEncoding(1257));
            Container container = new Container();
            for (int i = 3; i < Lines.Length; i++)
            {
                string[] Value = Lines[i].Split(';');
                string licencePlate = Value[0];
                string manufacturer = Value[1];
                string model = Value[2];
                DateTime yearOfManufacture = DateTime.Parse(Value[3]);
                DateTime technicvalInspection = DateTime.Parse(Value[4]);
                string fuel = Value[5];
                double consumption = double.Parse(Value[6]);

                Auto newAuto = new Auto(licencePlate, manufacturer, model, yearOfManufacture, technicvalInspection, fuel, consumption);

                container.Add(newAuto); 
            }

            register = new Register(container);
            register.FilialoInfo[0] = Lines[0];
            register.FilialoInfo[1] = Lines[1];
            register.FilialoInfo[2] = Lines[2];

            return register;
        }

        

        /// <summary>
        /// Spausdinami automobiliai į txt failą.
        /// </summary>
        /// <param name="fileName">Failo pavadinimas</param>
        /// <param name="header">Antraštė</param>
        public static void PrintAutosToTxt(string fileName, string header, Register register)
        {
            Container container = register.GetContainer();
            string[] Lines = new string[container.Count + 7];
            Lines[0] = new string('-', 171);
            Lines[1] = string.Format("|{0,-169}|", header);
            Lines[2] = new string('-', 171);
            Lines[3] = string.Format("| {0,-22} | {1,-10} | {2,-10} | {3,-30} | {4,-35} | {5,-10} | {6,-32} |", "Valstybinis numeris", "Gamintojas", "Modelis",
                "Pagaminimo metai ir mėnuo", "Techninės apžiūros galiojimo data", "Kuras", "Vidutinės kuro sąnaudos(100 km)");
            Lines[4] = new string('-', 171);
            for (int i = 0; i < container.Count; i++)
            {
                Auto auto = container.Get(i);
                Lines[i + 5] = auto.ToString();
            }
            Lines[container.Count + 5] = new string('-', 171);
            Lines[container.Count + 6] = new string(' ', 1);

            File.AppendAllLines(fileName, Lines);
        }

        /// <summary>
        /// Spausdinami automobiliai į konsolinį langą.
        /// </summary>
        /// <param name="header">Antraštė</param>
        /// <param name="container">Konteineris</param>
        public static void PrintAutos(string header, Container container)
        {
            Separator();
            Console.WriteLine("|{0,-169}|", header);
            Separator();
            Console.WriteLine("| {0,-22} | {1,-10} | {2,-10} | {3,-30} | {4,-35} | {5,-10} | {6,-32} |", "Valstybinis numeris", "Gamintojas", "Modelis",
                "Pagaminimo metai ir mėnuo", "Techninės apžiūros galiojimo data", "Kuras", "Vidutinės kuro sąnaudos(100 km)");
            Separator();
            for (int i = 0; i < container.Count; i++)
            {
                Auto auto = container.Get(i);
                Console.WriteLine(auto.ToString());
            }
            Separator();
            Console.WriteLine("");
        }

        /// <summary>
        /// Brukšniai lentelėms daryti.
        /// </summary>
        private static void Separator()
        {
            Console.WriteLine(new string('-', 171));
        }

        public static void PrintGasCars(string header, Container gasCars)
        {
            Separator();
            Console.WriteLine("|{0,-169}|", header);
            Separator();
            Console.WriteLine("| {0,-22} | {1,-10} | {2,-10} | {3,-30} |", "Valstybinis numeris", "Gamintojas", "Modelis",
                "Pagaminimo metai ir mėnuo");
            Separator();
            for (int i = 0; i < gasCars.Count; i++)
            {
                Auto auto = gasCars.Get(i);
                Console.WriteLine("| {0,-22} | {1,-10} | {2,-10} | {3,30:yyyy-MM-dd} |", auto.LicencePlate, auto.Manufacturer, auto.Model, auto.YearOfManufacture);
            }
            Separator();
        }

        /// <summary>
        /// Atspausdinamas automobilis į failą.
        /// </summary>
        /// <param name="auto">Automobilis</param>
        /// <param name="f1">Pirmas filialas</param>
        /// <param name="f2">Antras filialas</param>
        /// <param name="fileName">Failo pavadinimas</param>
        public static void AppendToCsv(Auto auto, string f1, string f2, string fileName)
        {
            string[] text = new string[1];
            text[0] = string.Format("{0};{1};{2}, {3}", auto.LicencePlate, auto.Model, f1, f2);
            File.AppendAllLines(fileName, text);
        }

        /// <summary>
        /// Atspausdinamas automobilis į duotą txt failą.
        /// </summary>
        /// <param name="fileName">Failo pavadinimas</param>
        /// <param name="auto">Automobilis</param>
        public static void AppendToTxt(string fileName, Auto auto)
        {
            string[] text = new string[1];
            text[0] = string.Format("| {0,-12} | {1,-10} | {2,-22} | {3,28:yyyy-MM-dd} |", auto.Manufacturer, auto.Model, auto.LicencePlate, auto.TechnicalInspection);
            File.AppendAllLines(fileName, text);
        }
        public static void AppendToTxt(string fileName, string[] Lines)
        {
            File.AppendAllLines(fileName, Lines);
        }
    }
}
