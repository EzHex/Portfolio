using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.IO;

namespace Kryziazodis
{
    public class Tasks
    {
        /// <summary>
        /// Suformuoja lentelės eilutę
        /// </summary>
        /// <param name="crosswordLines">Paduodama iš duomenų failo paimta eilutė</param>
        /// <param name="i">Eilutės indeksas</param>
        /// <param name="n">Kiek "stulpelių" yra eilutėje</param>
        /// <returns>Suformatuota eilutę, skirtą idėti į lentelę</returns>
        public static TableRow FormRow(List<string> crosswordLines, int i, int n)
        {
            string[] letters = crosswordLines[i].Split(';');

            TableRow row = new TableRow();

            for (int i2 = 0; i2 < n; i2++)
            {
                TableCell cell = new TableCell();
                cell.Text = letters[i2];
                row.Cells.Add(cell);
            }

            return row;
        }
        /// <summary>
        /// Suformatuoja visos lentelės eilutes iš viso kryžiažodžio dvimačio masyvo
        /// </summary>
        /// <param name="crosswordLines">Kryžiažodis</param>
        /// <param name="m">Eilučių skaičius</param>
        /// <param name="n">Stulpelių skaičius</param>
        /// <returns>Gražina suformatuotas eilutes, kad būtų galima įrašyti į lentelę</returns>
        public static List<TableRow> FormRow(string[,] crosswordLines, int m, int n)
        {
            List<TableRow> Rows = new List<TableRow>();

            for (int i = 0; i < m; i++)
            {
                TableRow row = new TableRow();
                for (int i2 = 0; i2 < n; i2++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = crosswordLines[i, i2].ToString();
                    row.Cells.Add(cell);
                }
                Rows.Add(row);
            }

            return Rows;
        }
        /// <summary>
        /// Formatuoja žodžių lentelę
        /// </summary>
        /// <param name="words">Žodžių sąrašas</param>
        /// <param name="i">Žodžio indeksas</param>
        /// <returns>Gražina stulpeliui skirtą eilutę</returns>
        public static TableRow FormWordRow(List<string> words, int i)
        {
            TableRow row = new TableRow();

            TableCell cell = new TableCell();
            cell.Text = (i + 1).ToString() + ". " + words[i];
            row.Cells.Add(cell);

            return row;
        }
        /// <summary>
        /// Iš duomenų failo randa kryžiažodžio žodžius
        /// </summary>
        /// <param name="allLines">Visos duomenų eilutės</param>
        /// <param name="k">Žodžių skaičius</param>
        /// <returns>Gražina žodžių sąrašą</returns>
        public static List<string> FindWords(string[] allLines, int k)
        {
            List<string> words = new List<string>();
            for (int i = 1; i <= k; i++)
            {
                words.Add(allLines[i]);
            }

            return words;
        }
        /// <summary>
        /// Iš duomenų failo išskiria (randa) kryžiažodžio eilutes
        /// </summary>
        /// <param name="allLines">Visos eilutes</param>
        /// <param name="k">Paieškos pradžios eilutė tarp visų duomenų</param>
        /// <returns>Kryžiažodžio eilučių sąrašą</returns>
        public static List<string> FindCrosswordLines(string[] allLines, int k)
        {
            List<string> crosswordLines = new List<string>();

            for (int i = k + 1; i < allLines.Length; i++)
            {
                crosswordLines.Add(allLines[i]);
            }

            return crosswordLines;
        }
        /// <summary>
        /// Iš kryžiažodžio eilučių sudaromas dvimatis masyvas
        /// </summary>
        /// <param name="crosswordLines">Kryžiažodžio eilutės</param>
        /// <param name="n">Stulpelių skaičius</param>
        /// <param name="m">Eilučiu skaičius</param>
        /// <returns>Gražina kryžiažodžio matricą</returns>
        public static string[,] FormCrosswordTable(List<string> crosswordLines, int n, int m)
        {
            string[,] crosswordTable = new string[m, n];

            for (int i = 0; i < m; i++)
            {
                string[] line = crosswordLines[i].Split(';');
                for (int i2 = 0; i2 < n; i2++)
                {
                    crosswordTable[i, i2] = line[i2];
                }
            }

            return crosswordTable;
        }
        /// <summary>
        /// Iškviečią pagrindinę funkciją, kuri pakeičia kryžiažodžio žodžius į žodžių eilės numerį
        /// </summary>
        /// <param name="crossword">Kryžiažodžis</param>
        public static void PerformTask(Crossword crossword)
        {
            crossword.DFSRecursiveSearch();
        }
        /// <summary>
        /// Randamos kontrolinės stulpelių sumos
        /// </summary>
        /// <param name="crossword">Kryžiažodis</param>
        /// <returns>Lentelės eilutę</returns>
        public static TableRow FindCheckSums(Crossword crossword)
        {
            TableRow result = new TableRow();
            List<int> count = new List<int>();

            for (int i = 0; i < crossword.n; i++)
            {
                count.Add(0);
            }

            for (int i = 0; i < crossword.m; i++)
            {
                for (int i2 = 0; i2 < crossword.n; i2++)
                {
                    count[i2] += int.Parse(crossword.crosswordTable[i, i2]);
                }
            }

            foreach (int number in count)
            {
                TableCell cell = new TableCell();
                cell.Text = number.ToString();
                result.Cells.Add(cell);
            }

            TableCell cellText = new TableCell();
            cellText.Text = "Kontrolinės sumos teisingumui patikrinti";
            result.Cells.Add(cellText);

            return result;
        }
        /// <summary>
        /// Sudaromos eilutes tekstiniam duomenų failui išspausdinti
        /// </summary>
        /// <param name="crossword">Kryžiažodis</param>
        /// <returns>Gražina suformatuotas eilutes</returns>
        public static string[] ConvertForTxt (Crossword crossword)
        {
            string[] allLines = new string[1 + crossword.k + crossword.m+2];
            allLines[0] = new string('-', 60);
            allLines[1] = string.Format("k = {0}, n = {1}, m = {2}", crossword.k, crossword.n, crossword.m);
            for (int i = 0; i < crossword.k; i++)
            {
                allLines[i + 2] = (i+1).ToString()+". " + crossword.words[i];
            }

            for (int i = 0; i < crossword.m; i++)
            {
                for (int i2 = 0; i2 < crossword.n; i2++)
                {
                    allLines[i + 2 + crossword.k] += (" " + crossword.crosswordTable[i, i2]);
                }
            }

            allLines[allLines.Length-1] = new string('-', 60);
            return allLines;
        }
    }
}