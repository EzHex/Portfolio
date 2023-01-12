using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

namespace Kryziazodis
{
    /// <summary>
    /// Pirmasis testas yra Kur3.txt faile
    /// Antrasis testas yra Kur4.txt faile
    /// 
    /// Failai įrašomi rankiniu būdų į Crosswords.xml failą, kad būtų matomį grafinės sąsajos sąraše.
    /// </summary>

    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Pirmasis mygtukas užkrauną kryžiažodį iš duomenų failų.
        /// Ir sukuria Duomenys.txt failą serveryje ( su duomenų lentele )
        /// </summary>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label3.Text = "";
            Button3.Visible = false;
            string path = "App_Data/" + DropDownList1.SelectedValue.ToString() + ".txt";
            string[] allLines = File.ReadAllLines(Server.MapPath(path));
            string[] variables = allLines[0].Split(' ');

            int k = int.Parse(variables[0]);
            int n = int.Parse(variables[1]);
            int m = int.Parse(variables[2]);

            List<string> crosswordLines = Tasks.FindCrosswordLines(allLines, k);

            for (int i = 0; i < m; i++)
            {
                Table1.Rows.Add(Tasks.FormRow(crosswordLines, i, n));
            }

            Crossword crossword = new Crossword(k, n, m, Tasks.FindWords(allLines, k),
                Tasks.FormCrosswordTable(crosswordLines, n, m));




            var dir = Server.MapPath("~/App_Data");
            var file = Path.Combine(dir, "Duomenys.txt");
            Directory.CreateDirectory(dir);
            if ( File.Exists(file))
            {
                File.Delete(file);
            }
            File.AppendAllLines(file, Tasks.ConvertForTxt(crossword));

            Session["crossword"] = crossword;

            for (int i = 0; i < k; i++)
            {
                Table2.Rows.Add(Tasks.FormWordRow(crossword.words, i));
            }

        }
        /// <summary>
        /// Antrasis mygtukas atlieka žodžių keitimą į skaitmenis
        /// </summary>
        protected void Button2_Click(object sender, EventArgs e)
        {
            Crossword crossword = (Crossword)Session["crossword"];
            if (crossword != null)
            {
                Label3.Text = "";
                Tasks.PerformTask(crossword);

                List<TableRow> Lines = Tasks.FormRow(crossword.crosswordTable, crossword.m, crossword.n);

                foreach (TableRow row in Lines)
                {
                    Table1.Rows.Add(row);
                }

                Table1.Rows.Add(Tasks.FindCheckSums(crossword));

                for (int i = 0; i < crossword.k; i++)
                {
                    Table2.Rows.Add(Tasks.FormWordRow(crossword.words, i));
                }

                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "Duomenys.txt");
                
                File.AppendAllLines(file, Tasks.ConvertForTxt(crossword));

                Button3.Visible = true;
            }
            else
            {
                Label3.Text = "Išsirinkite ir sugeneruokite kryžiažodį!";
            }
        }
        /// <summary>
        /// Trečiasis mygtukas suteikia atsakytam kryžiažodžiui spalvas.
        /// </summary>
        protected void Button3_Click(object sender, EventArgs e)
        {
            Crossword crossword = (Crossword)Session["crossword"];
            List<Color> colors = new List<Color>();
            Random rnd = new Random();
            int num = 1;

            foreach (string word in crossword.words)
            {
                TableRow wordTableRow = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = num.ToString() + ". " + word;
                cell.ForeColor = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                colors.Add(cell.ForeColor);
                wordTableRow.Cells.Add(cell);

                Table2.Rows.Add(wordTableRow);
                num++;
            }


            for (int i = 0; i < crossword.m; i++)
            {
                TableRow crosswordTableRow = new TableRow();
                for (int i2 = 0; i2 < crossword.n; i2++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = crossword.crosswordTable[i, i2];
                    cell.ForeColor = colors[int.Parse(cell.Text) - 1];
                    crosswordTableRow.Cells.Add(cell);
                }
                Table1.Rows.Add(crosswordTableRow);
            }
            Table1.Rows.Add(Tasks.FindCheckSums(crossword));
        }
    }
}