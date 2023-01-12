using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PasikartojantysZodziai
{
    class InOut
    {
        /// <summary>
        /// Nuskaitomi duomenys ir atliekamos visi nurodymai.
        /// </summary>
        /// <param name="fin">Duomenų failas</param>
        /// <param name="fout">Pirmas rezultatų failas</param>
        /// <param name="fout2">Antras rezultatų failas</param>
        /// <param name="punctuations">Skyriklių masyvas</param>
        public static void Process(string fin, string fout, string fout2, char[] punctuations)
        {
            string[] InputLines = File.ReadAllLines(fin, Encoding.GetEncoding(1257));

            List<Line> Lines = PutLinesInClass(InputLines);

            Dictionary<string, int> WordRepetitions = PutWordsInDictionary(Lines, punctuations);


            using (var writer = File.CreateText(fout))
            {
                writer.WriteLine("| {0,-15} | {1,-25} |", "Žodis", "Pasikartojimų skaičius");
                writer.WriteLine(new string('-', 47));
                int tempCount = 0;
                int temp = 0;
                foreach (var item in WordRepetitions.OrderByDescending(key => key.Value).ThenBy(key => key.Key))
                {
                    temp++;
                    if (item.Value > 1)
                    {
                        writer.WriteLine("| {0,-15} | {1,25} |", item.Key, item.Value);
                        tempCount++;
                    }
                    if (temp == 10 || item.Value < 2)
                    {
                        break;
                    }
                }
                if (tempCount == 0)
                {
                    writer.WriteLine("| {0, -43} |", "Nėra pasikartojančių žodžių");
                }
                writer.WriteLine(new string('-', 47));

                Line sentence = new Line();
                int numberOfLongestLine = FindLongestSentence(Lines, ref sentence, punctuations);
                writer.WriteLine("");
                writer.WriteLine("Ilgiausias sakinys: ");
                writer.WriteLine(sentence.Text);
                writer.WriteLine("Simbolių skaičius: {0}, žodžių skaičius: {1}", sentence.Text.Length, sentence.Count);
                writer.WriteLine("Vieta: {0} eilutė", numberOfLongestLine + 1);

            }


            List<StringBuilder> Result = CreateStringBuilderList(Lines);
            int index = 0;
            bool done = false;
            while (true)
            {
                int capacity = FindCapacityForWordCollumn(Lines, index, ref done);

                if (capacity != 0)
                {
                    Result = CorrectTheLines(Result, Lines, index, capacity + 2);
                }

                index++;

                if (done) break;
            }

            using (var writer = File.CreateText(fout2))
            {
                foreach (StringBuilder str in Result)
                {
                    writer.WriteLine(str);
                }
            }
        }

        /// <summary>
        /// Sukuriamas sąrašas StringBuilder tipo objektų.
        /// </summary>
        /// <param name="Lines">Siunčiamos visos eilutės dėl sąrašo dydžio</param>
        /// <returns>Gražinamas sąrašas</returns>
        private static List<StringBuilder> CreateStringBuilderList(List<Line> Lines)
        {
            List<StringBuilder> Result = new List<StringBuilder>();
            foreach (Line line in Lines)
            {
                Result.Add(new StringBuilder());
            }

            return Result;
        }
        /// <summary>
        /// Sutvarkomos eilutės, joms duodamas ilgis.
        /// </summary>
        /// <param name="Result">Sąrašas su galutinių rezultatu</param>
        /// <param name="Lines">Pradinės eilutės</param>
        /// <param name="index">Kelinta žodį dabar reikia tvarkyti, jo indeksas</param>
        /// <param name="capacity">Kokio ilgio turi būti visi žodžiai, atsižvelgiama pagal ilgiausia</param>
        /// <returns>Gražina sąrašą</returns>
        private static List<StringBuilder> CorrectTheLines(List<StringBuilder> Result, List<Line> Lines, int index, int capacity)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (index < Lines[i].Count)
                {
                    string word = Lines[i].SplitText[index];
                    while (word.Length != capacity)
                    {
                        word += " ";
                    }
                    Result[i].Append(word);
                }
            }

            return Result;
        }
        /// <summary>
        /// Randamas ilgiausias žodis ir nustatomas stulpelio dydis
        /// </summary>
        /// <param name="Lines">Visų eilučių sąrašas</param>
        /// <param name="index">Kelinta stulpeli reikia tvarkyti</param>
        /// <param name="done">Ar sutvarkyti visi stulpeliai</param>
        /// <returns>Gražinamas ilgiausio žodžio ilgis</returns>
        private static int FindCapacityForWordCollumn(List<Line> Lines, int index, ref bool done)
        {
            int capacity = 0;
            Regex expression = new Regex(@"[\w]+.");

            for (int i = 0; i < Lines.Count; i++)
            {
                if (index < Lines[i].Count)
                {
                    Lines[i].SplitText[index] = (expression.Match(Lines[i].SplitText[index])).ToString();
                    int temp = Lines[i].SplitText[index].Length;
                    if (temp > capacity) capacity = temp;
                }
            }

            if (capacity == 0) done = true;
            return capacity;
        }
        /// <summary>
        /// Randamas ilgiausias sakinys.
        /// </summary>
        /// <param name="Lines">Visos eilutės</param>
        /// <param name="sentence">Ilgiausias sakinys</param>
        /// <param name="punctuations">Skyrikliai</param>
        /// <returns>Gražinamas indeksas kur sakinys prasideda pradinėse eilutėse</returns>
        private static int FindLongestSentence(List<Line> Lines, ref Line sentence, char[] punctuations)
        {
            int index = 0;

            StringBuilder wholeTextInOneLine = new StringBuilder();
            Regex expression = new Regex(@"[\w]+.");
            foreach (Line line in Lines)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    string text = (expression.Match(line.SplitText[i])).ToString();
                    wholeTextInOneLine.Append(text + " ");
                }

            }

            string[] Sentences = wholeTextInOneLine.ToString().Split('.');
            List<Line> SentencesInClass = PutLinesInClass(Sentences);
            int maxi = 0;

            for (int i = 0; i < Sentences.Length; i++)
            {
                if (SentencesInClass[i].Count > maxi)
                {
                    maxi = SentencesInClass[i].Count;
                    index = i;
                }
            }

            sentence = SentencesInClass[index];

            index = PrimaryIndex(Lines, sentence);

            return index;
        }
        /// <summary>
        /// Randama sakinio pradžia pradinėse eilutės pagal 3 pirmus sakinio žodžius.
        /// </summary>
        /// <param name="Lines">Pradinės eilutės</param>
        /// <param name="sentence">Ilgiausias sakinys</param>
        /// <returns>Gražinamas indeksas kur prasideda sakinys</returns>
        private static int PrimaryIndex(List<Line> Lines, Line sentence)
        {
            int index = 0;

            StringBuilder first3WordsOfSentence = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                first3WordsOfSentence.Append(sentence.SplitText[i] + " ");
            }

            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Text.Contains(first3WordsOfSentence.ToString().TrimEnd()))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
        /// <summary>
        /// Pradiniai duomenys sudedami į klasę, kad būtų lengviau manipuliuoti ir formatuoti tekstą.
        /// </summary>
        /// <param name="InputLines">Eilutės iš duomenų failo</param>
        /// <returns></returns>
        private static List<Line> PutLinesInClass(string[] InputLines)
        {
            List<Line> Lines = new List<Line>();
            foreach (string line in InputLines)
            {
                Line a = new Line(line.Trim());
                Lines.Add(a);
            }
            return Lines;
        }
        /// <summary>
        /// Metodas VIENAM žodžiui nuo skyriklių apvalyti.
        /// </summary>
        /// <param name="word">Žodis</param>
        /// <param name="punctuations">Skyrikliai</param>
        /// <returns>Grąžinama žodis be skyriklių</returns>
        private static string FullyTrimWord(string word, char[] punctuations)
        {
            string newWord = word;
            bool done;

            while (true)
            {
                done = true;
                if (newWord.IndexOfAny(punctuations) != -1)
                {
                    newWord = newWord.Trim(punctuations);
                    done = false;
                }

                if (done) break;
            }


            return newWord;
        }

        /// <summary>
        /// Sudedami skirtingi žodžiai, kad būtų galima rasti pasikartojančius.
        /// </summary>
        /// <param name="Lines">Eilutės</param>
        /// <param name="punctuations">Skyrikliai</param>
        /// <returns>Gražinamas Dictionary sąrašas su pasikartojimo kiekiais</returns>
        private static Dictionary<string, int> PutWordsInDictionary(List<Line> Lines, char[] punctuations)
        {
            Dictionary<string, int> WordRepetitions = new Dictionary<string, int>();

            foreach (Line line in Lines)
            {
                string[] l = line.SplitText;
                for (int i = 0; i < l.Length; i++)
                {
                    string temp = FullyTrimWord(l[i], punctuations);
                    if (!WordRepetitions.ContainsKey(temp.ToLower()))
                    {
                        WordRepetitions.Add(temp.ToLower(), 1);
                    }
                    else
                    {
                        WordRepetitions[temp.ToLower()]++;
                    }
                }
            }

            return WordRepetitions;
        }
    }
}
