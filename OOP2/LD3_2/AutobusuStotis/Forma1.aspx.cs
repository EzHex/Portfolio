using System;
using System.Diagnostics;
using System.IO;

namespace AutobusuStotis
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && FileUpload2.HasFile)
            {
                Label6.Text = "";
                Label12.Text = "";
                Label13.Visible = false;

                string pathA = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Data/") + pathA);

                string pathB = Path.GetFileName(FileUpload2.PostedFile.FileName);
                FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Data/") + pathB);

                pathA = "~/Data/" + pathA;
                pathB = "~/Data/" + pathB;

                string[] fileA = File.ReadAllLines(Server.MapPath(pathA));
                string[] fileB = File.ReadAllLines(Server.MapPath(pathB));

                try
                {
                    LinkedList<Bus> busLinkedList = Tasks.FormLinkedList(fileA);
                    LinkedList<Price> priceLinkedList = Tasks.FormPriceList(fileB);

                    Session["Bus"] = busLinkedList;
                    Session["Price"] = priceLinkedList;

                    Tasks.WriteToTable(busLinkedList, Table1, priceLinkedList);
                    Label3.Text = "Esama stotis: " + busLinkedList.HeadCity;

                    Label2.Visible = true;
                    TextBox1.Visible = true;
                    TextBox2.Visible = true;
                    TextBox3.Visible = true;
                    DropDownList2.Visible = true;
                    Label4.Visible = true;
                    Label5.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;

                    var dir = Server.MapPath("~/App_Data");
                    var file = Path.Combine(dir, "DuomenysIrRezultatai.txt");
                    Directory.CreateDirectory(dir);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    File.AppendAllLines(file, Tasks.ConvertForTxt(busLinkedList, "Pradiniai autobusų duomenys"));
                    File.AppendAllLines(file, Tasks.ConvertForTxt(priceLinkedList, "Pradiniai kainų duomenys"));
                }
                catch (Exception)
                {
                    Label12.Text = "Failuose esanti informacija yra blogo formato";     
                }
            }
            else
            {
                Label12.Text = "Reikia pasirinkti duomenų failus";

                Label2.Visible = false;
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                TextBox3.Visible = false;
                DropDownList2.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;

                Label3.Text = "";
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string city = null;
            if (Tasks.NotDigit(TextBox1.Text))
            {
                city = TextBox1.Text.ToLower();
                city = city.Replace(city[0],
                    city[0].ToString().ToUpper().ToCharArray()[0]);
                Label8.Text = "";
            }
            else
            {
                Label8.Text = "Miesto pavadinimas neteisingas";
            }
            
            DateTime departureTime = new DateTime();
            DateTime arrivalTime = new DateTime();
            if (Tasks.IsItTime(TextBox2.Text) && Tasks.IsItTime(TextBox3.Text))
            {
                Label7.Text = "";
                departureTime = DateTime.Parse(TextBox2.Text);
                arrivalTime = DateTime.Parse(TextBox3.Text);
            }
            else
            {             
                Label7.Text = "Blogas laiko formatas";
            }

            if (departureTime != DateTime.MinValue && arrivalTime != DateTime.MinValue &&
                city != null)
            {
                string day = DropDownList2.SelectedValue;

                LinkedList<Bus> selectedList = Tasks.FindSelectedCities((LinkedList<Bus>)Session["Bus"],
                    city, departureTime, arrivalTime, day);

                selectedList.Sort();
                selectedList.Begin();
                if (selectedList == null || !selectedList.Exist())
                {
                    Label6.Text = "Tokių maršrutų nėra";
                }
                else
                {
                    Tasks.WriteToTable(selectedList, Table1, (LinkedList<Price>)Session["Price"]);
                }

                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "DuomenysIrRezultatai.txt");
                File.AppendAllLines(file, Tasks.ConvertForTxt(selectedList, "Atrinkti duomenys"));
            }
            else
            {
                Tasks.WriteToTable((LinkedList<Bus>)Session["Bus"],
                    Table1, (LinkedList<Price>)Session["Price"]);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label6.Text = "";
            LinkedList<Bus> selectedList = Tasks.FindMostPopularCityList((LinkedList<Bus>)Session["Bus"]);

            selectedList = Tasks.DeleteTransits(selectedList);
            selectedList.Sort();

            selectedList.Begin();
            if (selectedList.Exist())
            {
                Tasks.WriteToTable(selectedList, Table2, (LinkedList<Price>)Session["Price"]);
                Label13.Visible = false;
            }
            else
            {
                Label13.Visible = true;
            }
            

            Label2.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            DropDownList2.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            Button2.Visible = false;

            var dir = Server.MapPath("~/App_Data");
            var file = Path.Combine(dir, "DuomenysIrRezultatai.txt");

            File.AppendAllLines(file, Tasks.ConvertForTxt(selectedList,
                (LinkedList<Price>)Session["Price"], "Populiariausias maršrutas su kainomis"));
        }
    }
}