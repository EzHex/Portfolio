using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace Serveris
{
    public partial class Forma1 : System.Web.UI.Page
    {
        /// <summary>
        /// Loads tables and dropdownlist default value.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownList1.Items.Count == 0) DropDownList1.Items.Add(new ListItem("-"));

            if (Session["registers"] != null) Task.CreateTable((List<DayRegister>)Session["registers"], Panel1);

            if (Session["servers"] != null) Task.CreateTableForServer((List<Server>)Session["servers"], Panel1);
        }
        /// <summary>
        /// Read data from files, calling methods for writing to table and writing to txt file.
        /// </summary>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label2.Visible = false;
            this.Panel1.Controls.Clear();
            var dir = Server.MapPath("~/App_Data");
            var file = Path.Combine(dir, "Rezultatai.txt");
            Directory.CreateDirectory(dir);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            string[] allFiles = Directory.GetFiles(dir, "diena*.txt");
            string serverPath = Server.MapPath("~/App_Data/Servers.txt");
            List<DayRegister> registers = new List<DayRegister>();
            List<Server> servers = new List<Server>();
            bool ok = true;
            try
            {
                registers = Task.ReadFiles(allFiles);
                servers = Task.ReadFile(serverPath);
            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
                Label2.Visible = true;
                ok = false;
            }

            if (ok)
            {
                Task.CreateTable(registers, Panel1);
                Task.CreateTableForServer(servers, Panel1);
                Task.AddServerNamesToDropDownList(servers, DropDownList1);

                File.AppendAllLines(file, Task.LinesForTxt("Pradiniai registrų duomenys", registers));
                File.AppendAllLines(file, Task.LinesForTxt("Pradiniai serverių duomenys", servers));


                DropDownList1.Visible = true;
                Button2.Visible = true;

                Session["registers"] = registers;
                Session["servers"] = servers;
            }
        }
        /// <summary>
        /// Takes all the data from registers by the selected server name
        /// and calls methods to create TreeView and write to txt file.
        /// </summary>
        protected void Button2_Click(object sender, EventArgs e)
        {
            bool ok = true;
            TreeView1.Nodes.Clear();
            List<DayRegister> registers = (List<DayRegister>)Session["registers"];
            string name = DropDownList1.SelectedValue;
            List<Day> days = new List<Day>();

            try
            {
                days = Task.FormTreeView(name, registers, TreeView1);
            }
            catch (Exception)
            {
                Label2.Text = "Nepasirinktas serveris";
                Label2.Visible = true;
                ok = false;
            }

            if (ok)
            {
                Label3.Visible = true;
                Label3.Text = name;
                Button3.Visible = true;
                TreeView1.Visible = true;
                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "Rezultatai.txt");
                File.AppendAllLines(file, Task.LinesForTxt("Išrinktos dienos pagal serverio vardą", days));
            }
        }
        /// <summary>
        /// Sorts existing TreeView alphabetically
        /// </summary>
        protected void Button3_Click(object sender, EventArgs e)
        {
            Task.SortTreeView(TreeView1);
        }

    }
}