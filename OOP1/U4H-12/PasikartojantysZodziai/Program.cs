using System;

namespace PasikartojantysZodziai
{
    class Program
    {
        static void Main(string[] args)
        {
            //Duomenų faile sakinys turi būti bent iš 3 žodžių, jeigu ilgesnis, tai pirmi trys žodžiai turi būti vienoje eilutėje.
            //Nes ilgiausias sakinys yra ieškomas pagal tris pirmus žodžius, randa sakinį su ieškomais 3 žodžiais.
                //Knyga.txt (yra keli pasikartojantys žodžiai)
                //Knyga2.txt (nėra pasikartojančių žodžių)

            const string InputFile = @"Knyga.txt";
            const string OutputFile = @"Rodikliai.txt";
            const string OutputFile2 = @"ManoKnyga.txt";
            char[] Punctuations = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '\t', '\'', '"' };

            InOut.Process(InputFile, OutputFile, OutputFile2, Punctuations);

            Console.ReadKey();
        }
    }
}
