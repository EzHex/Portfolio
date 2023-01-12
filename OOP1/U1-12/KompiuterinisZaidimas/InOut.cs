using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KompiuterinisZaidimas
{
    class InOut
    {
        /// <summary>
        /// Nuskaitomi herojai ir jiems priskiriamos jų charakteristikos iš duoto failo
        /// </summary>
        /// <param name="fileName"> duotas failas </param>
        /// <returns> gražinamas herojų sąrašas </returns>
        public static List<Hero> ReadHeroes(string fileName) 
        {
            List<Hero> Heroes = new List<Hero>();
            if (File.Exists(fileName))
            {
                string[] Lines = File.ReadAllLines(fileName, Encoding.GetEncoding(1257));
                foreach (string line in Lines)
                {
                    string[] Values = line.Split(';');
                    string name = Values[0];
                    string race = Values[1];
                    string cls = Values[2];
                    double healthPoints = double.Parse(Values[3]);
                    double mana = double.Parse(Values[4]);
                    double damagePoints = double.Parse(Values[5]);
                    double defensePoints = double.Parse(Values[6]);
                    double power = double.Parse(Values[7]);
                    double agility = double.Parse(Values[8]);
                    double intelligence = double.Parse(Values[9]);
                    string specialPower = Values[10];

                    Hero newHero = new Hero(name, race, cls, healthPoints, mana, damagePoints, defensePoints,
                        power, agility, intelligence, specialPower);
                    Heroes.Add(newHero);

                }
            }

            return Heroes;
        }

        /// <summary>
        /// Tik elfų spausdinimas į failą
        /// </summary>
        /// <param name="Heroes"> visų herojų sąrašas </param>
        public static void PrintElves (List<Hero> Heroes)
        {
            string[] lines = new string[Heroes.Count + 1];
            lines[0] = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                "Vardas", "Rasė", "Klasė", "Gyvybės taškai", "Mana", "Žalos taškai", "Gynybos taškai", "Jėga",
                "Vikrumas", "Intelektas", "Ypatinga galia");
            int i = 1;
            bool noElves = true;
            foreach (Hero h in Heroes)//spausdinami visi elfai
            {
                if ( h.Race.ToLower() == "elfas")
                {
                    noElves = false;
                    lines[i] = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                    h.Name, h.Race, h.Cls, h.HealthPoints, h.Mana, h.DamagePoints, h.DefensePoints,
                    h.Power, h.Agility, h.Intelligence, h.SpecialPower);
                    i++;
                }
            }

            if (noElves)
            {
                lines[0] = "Elfų nėra";
            }
            File.WriteAllLines("Elfai.csv", lines, Encoding.GetEncoding(1257));

        }
        /// <summary>
        /// Spausdinami geriausi herojai į konsolę.
        /// </summary>
        /// <param name="BestHeroes"> Sąrašas geriausiu herojų </param>
        public static void PrintBestHeroes( List<Hero> BestHeroes)
        {
            Console.WriteLine("Geriausiomis charakteristikomis pasižymi: ");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-10} | {1,-10} | {2,-14} | {3,-10} | {4,-10} | {5,-10} |", "Vardas", "Rasė", "Klasė",
                "Jėga", "Vikrumas", "Intelektas");
            Console.WriteLine("-----------------------------------------------------------------------------------");

            foreach (Hero hero in BestHeroes)
            {
                Console.WriteLine("| {0,10} | {1,10} | {2,14} | {3,10} | {4,10} | {5,10} |",
                    hero.Name, hero.Race, hero.Cls, hero.Power, hero.Agility, hero.Intelligence);
            }
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        /// <summary>
        /// Spausdina į konsolę didžiausias rases.
        /// </summary>
        /// <param name="maxi"> didžiausios rasės dydis </param>
        /// <param name="Races"> rasių sąrašas </param>
        /// <param name="Heroes"> herojų sąrašas </param>

        public static void PrintLargestGroups( int maxi, List<Races> Races, List<Hero> Heroes)
        {
            Console.WriteLine("Daugiausiai herojų yra iš rasės");
            Console.WriteLine();


            foreach (Races r in Races) // spausdinamos didžiausios rasės
            {
                if (r.NumberOfMembers == maxi)
                {
                    Console.WriteLine("{0}:", r.RaceName);
                    Console.WriteLine("--------------");
                    Console.WriteLine("| {0,-10} |", "Vardas");
                    Console.WriteLine("--------------");
                    foreach (Hero h in Heroes)
                    {
                        if (h.Race == r.RaceName)
                        {
                            Console.WriteLine("| {0,10} |", h.Name);
                        }
                    }
                    Console.WriteLine("--------------");
                    Console.WriteLine();
                }

            }
        }

    }
}
