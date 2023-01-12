using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KrepsinioRinktine
{
    class InOut
    {
        /// <summary>
        /// Skaitoma informacija iš failo ir sudaromi žaidėjai.
        /// </summary>
        /// <param name="fileName"> Failo pavadinimas </param>
        /// <returns> Gražinamas žaidėjų sąrašas </returns>
        public static List<Player> ReadPlayers ( string fileName )
        {
            List<Player> Players = new List<Player>();

            if (File.Exists(fileName))
            {
                string[] Lines = File.ReadAllLines(fileName, Encoding.GetEncoding(1257));
                for (int i = 3; i < Lines.Length; i++)
                {
                    string[] Value = Lines[i].Split(';');
                    string name = Value[0];
                    string surname = Value[1];
                    DateTime birthDate = DateTime.Parse(Value[2]);
                    int height = int.Parse(Value[3]);
                    string position = Value[4];
                    string club = Value[5];
                    Invited invite;
                    Enum.TryParse(Value[6], out invite);
                    Captain captain;
                    Enum.TryParse(Value[7], out captain);

                    Player player = new Player(name, surname, birthDate, height, position, club, invite, captain);

                    Players.Add(player);
                }
            }
            return Players;
        }

        /// <summary>
        /// Spausdinami pradiniai duomenys.
        /// </summary>
        /// <param name="players"> Visų žaidėjų sąrašas </param>
        public static void PrintToConsole (List<Player> players)
        {
            Console.WriteLine("Pradiniai duomenys: ");
            Console.WriteLine("------------------------------------------------------------" +
                "--------------------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-20} | {1,-20} | {2,-25} | {3,-10} | {4,-10} | {5, -10} | {6, -22} | {7, -10} |",
                "Vardas", "Pavarde", "Gimimo data", "Ūgis", "Pozicija", "Klubas", "Pakviestas į rinktinę", "Kapitonas");
         
            Console.WriteLine("------------------------------------------------------------" +
                "--------------------------------------------------------------------------------------------");
            foreach (Player p in players)
            {
                Console.WriteLine("| {0,20} | {1,20} | {2,25} | {3,10} | {4,10} | {5, 10} | {6, 22} | {7, 10} |", p.Name, p.Surname, p.BirthDate, p.Height, p.Position,
                    p.Club, p.Invite, p.Captain);
            }
            Console.WriteLine("------------------------------------------------------------" +
                "--------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Išrašomi duoto sąrašo žaidėjai į konsolę (vardas, pavardė, pozicija).
        /// </summary>
        /// <param name="players"> Žaidėjų sąrašas </param>
        /// <param name="text"> Papildomas tekstas, jeigu reikia </param>
        /// <param name="alternativeText"> Priešingas tekstas, jeigu reikia </param>
        public static void WriteToConsole (List<Player> players, string text, string alternativeText)
        {
            if (players.Capacity != 0)
            {
                Console.WriteLine(text);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("| {0,-20} | {1,-20} | {2,-10} |", "Vardas", "Pavarde", "Pozicija");
                Console.WriteLine("------------------------------------------------------------");
                foreach (Player p in players)
                {
                    Console.WriteLine("| {0,20} | {1,20} | {2,10} |", p.Name, p.Surname, p.Position);
                }
                Console.WriteLine("------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine(alternativeText);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Išrašomi duoto sąrašo žaidėjai į konsolę (vardas, pavardė, amžius).
        /// </summary>
        /// <param name="players"> Žaidėjų sąrašas </param>
        public static void WriteHighestPlayersToConsole (List<Player> players)
        {
            Console.WriteLine("Aukščiausių žaidėjų sąrašas");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("| {0,-20} | {1,-20} | {2,-10} |", "Vardas", "Pavarde", "Amžius");
            Console.WriteLine("------------------------------------------------------------");

            foreach (Player p in players)
            {
                Console.WriteLine("| {0,20} | {1,20} | {2,10} |", p.Name, p.Surname, p.Age);
            }
            Console.WriteLine("------------------------------------------------------------");
        }

        /// <summary>
        /// Išrašomas žaidėjų sąrašas į csv failą.
        /// </summary>
        /// <param name="filename"> Failo pavadinimas </param>
        /// <param name="players"> Žaidėjų sąrašas </param>
        public static void WriteToCSV ( string filename, List<Player> players)
        {
            string[] lines = new string[players.Count + 1];
            lines[0] = String.Format("{0};{1};{2}", "Vardas", "Pavardė", "Ūgis");
            for (int i = 0; i < players.Count; i++)
            {
                lines[i+1] = String.Format("{0};{1};{2}", players[i].Name, players[i].Surname, players[i].Height);
            }
            if (players.Count == 0) lines[0] = "Aukštaūgių nėra";
            File.WriteAllLines(filename, lines, Encoding.GetEncoding(1257));
        }

    }
}
