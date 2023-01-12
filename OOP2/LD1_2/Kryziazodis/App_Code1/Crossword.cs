using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kryziazodis
{
    public class Crossword
    {
        public int k { get; set; }
        public int n { get; set; }
        public int m { get; set; }
        public List<string> words { get; set; }
        public string[,] crosswordTable { get; set; }
        /// <summary>
        /// Konstruktorius kryžiažodžio išsaugojimui ir darbui su juo.
        /// </summary>
        /// <param name="k">Žodžių skaičius</param>
        /// <param name="n">Stulpelių skaičius</param>
        /// <param name="m">Eilučių skaičius</param>
        /// <param name="words">Kryžiažodžio žodžiai</param>
        /// <param name="crosswordTable">Kryžiažodis</param>
        public Crossword(int k, int n, int m, List<string> words, string[,] crosswordTable)
        {
            this.k = k;
            this.n = n;
            this.m = m;
            this.words = words;
            this.crosswordTable = crosswordTable;
        }
        /// <summary>
        /// Kryžiažodžiui išspręsti skirtas metodas, eina kas žodį ir jį verčia į skaitmenis
        /// </summary>
        public void DFSRecursiveSearch()
        {
            int num = 1;

            foreach (string word in this.words)
            {
                int index = 0;

                Stack<char> tempWord = new Stack<char>();

                for (int i2 = 0; i2 < this.m; i2++)
                {
                    for (int i3 = 0; i3 < this.n; i3++)
                    {
                        if (DFSSearch(word.ToUpper(), index, tempWord, i2, i3, num))
                        {
                            break;
                        }
                    }
                }
                num++;
            }
        }

        /// <summary>
        /// Rekursinė žodžio paieška kryžiažodžio lentelėje
        /// </summary>
        /// <param name="word">Ieškomas žodis</param>
        /// <param name="index">Ieškomo žodžio raidės indeksas</param>
        /// <param name="tempWord">Laikinas žodis, kuris yra sudaromas paieškos metu,
        /// kai šis žodis sutampa su ieškomuoju, paieška baigta</param>
        /// <param name="i">Kryžiažodžio lentelėje eilutes reikšmė</param>
        /// <param name="j">Kryžiažodžio lentelėje stulpelio reikšmė</param>
        /// <param name="num">Žodžio eilės numeris</param>
        /// <returns>Gražina ar rasta raidė žodis yra teisingas ar ne</returns>
        private bool DFSSearch(string word, int index, Stack<char> tempWord,
            int i, int j, int num)
        {
            if (IsDigit(this.crosswordTable[i, j].ToCharArray()[0]))
            {
                return false;
            }

            if (index >= word.Length)
            {
                return true;
            }

            if (word[index] != this.crosswordTable[i, j].ToCharArray()[0])
            {
                return false;
            }
            tempWord.Push(this.crosswordTable[i, j].ToCharArray()[0]);

            if (word == StackToString(tempWord))
            {
                return true;
            }

            if (i + 1 < this.m && DFSSearch(word, index + 1, tempWord, i + 1, j, num))
            {
                this.crosswordTable[i, j] = num.ToString();
                return true;
            }

            if (j + 1 < this.n && DFSSearch(word, index + 1, tempWord, i, j + 1, num))
            {
                this.crosswordTable[i, j] = num.ToString();
                return true;
            }

            if (i - 1 >= 0 && DFSSearch(word, index + 1, tempWord, i - 1, j, num))
            {
                this.crosswordTable[i, j] = num.ToString();
                return true;
            }

            if (j - 1 >= 0 && DFSSearch(word, index + 1, tempWord, i, j - 1, num))
            {
                this.crosswordTable[i, j] = num.ToString();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Stack'e laikomas laikinasis žodis paverčiamas į string'ą
        /// </summary>
        /// <param name="word">Laikinasis žodis</param>
        /// <returns>Gražina laikinąjį žodį paverstą į string'ą</returns>
        private string StackToString(Stack<char> word)
        {
            string result = "";
            char letter;
            int n = word.Count;
            for (int i = 0; i < n; i++)
            {
                letter = word.Pop();
                result = letter + result;
            }
            return result;
        }
        /// <summary>
        /// Tikrina ar tai skaičius
        /// </summary>
        /// <param name="a">Kryžiažodyje esantis simbolis</param>
        /// <returns>Ar tai skaičius ar ne</returns>
        private bool IsDigit(char a)
        {
            Regex expression = new Regex(@"\d");
            if (expression.IsMatch(a.ToString()))
            {
                return true;
            }
            return false;
        }
    }
}