using System;
using System.Collections.Generic;
using System.Text;

namespace KrepsinioRinktine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.GetEncoding(1257);
            Console.OutputEncoding = Encoding.GetEncoding(1257);

            // Programa veiks tik tada, kai du failai turės bent po vieną žaidėją.
            // Žaidėjų vardai unikalus.

            // 6 Žalgiriečiai ir vienas aukščiausias žaidėjas. 2 Aukštaūgiai.
                //Failai: Krepsininkai2020.csv ir Krepsininkai2019.csv

            // Žalgiriečių nėra ir 4 aukščiausi žaidėjai. Aukštaūgių nėra.
                //Failai: Krepsininkai2018.csv ir Krepsininkai2017.csv

            List<Player> Players = InOut.ReadPlayers(@"Krepsininkai2018.csv");
            List<Player> LastYearPlayers = InOut.ReadPlayers(@"Krepsininkai2017.csv");

            PlayersRegister playersRegister = new PlayersRegister(Players);
            playersRegister.Add(LastYearPlayers);


            if (Players.Capacity != 0 && LastYearPlayers.Capacity != 0)
            {
                InOut.PrintToConsole(playersRegister.GetList());

                InOut.WriteToConsole(Task.SortZalgirisPlayers(playersRegister.GetList()), "Kauno Žalgirio žaidėjų sąrašas",
                    "Žaidėjų iš Kauno Žalgirio nėra.");

                InOut.WriteHighestPlayersToConsole(Task.HighestPlayers(playersRegister.GetList()));

                InOut.WriteToCSV(@"Aukštaūgiai.csv", Task.PlayersOver2M(playersRegister.GetList()));
            }
            else Console.WriteLine("Žaidėjų failai tušti arba jų nėra.");
                

            Console.ReadKey();
        }
    }
}
