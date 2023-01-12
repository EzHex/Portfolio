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
            Label6.Text = "";
            string pathA = "App_Data/" + DropDownList1.SelectedValue.ToString() + "a.txt";
            string[] fileA = File.ReadAllLines(Server.MapPath(pathA));
            BusLinkedList busLinkedList = Tasks.FormLinkedList(fileA);

            string pathB = "App_Data/" + DropDownList1.SelectedValue.ToString() + "b.txt";
            string[] fileB = File.ReadAllLines(Server.MapPath(pathB));
            PriceLinkedList priceLinkedList = Tasks.FormPriceList(fileB);

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
            File.AppendAllLines(file, Tasks.ConvertForTxt(busLinkedList));
            File.AppendAllLines(file, Tasks.ConvertForTxt(priceLinkedList));

            Session["file"] = file;
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
            if (Tasks.IsItTime(TextBox2.Text))
            {
                Label7.Text = "";
                departureTime = DateTime.Parse(TextBox2.Text);
            }
            else
            {
                Label7.Text = "Blogas laiko formatas";
            }
                

            if (Tasks.IsItTime(TextBox3.Text))
            {
                arrivalTime = DateTime.Parse(TextBox3.Text);
                Label7.Text = "";
            } 
            else
            {
                Label7.Text = "Blogas laiko formatas";
            }

            if (departureTime != DateTime.MinValue && arrivalTime != DateTime.MinValue &&
                city != null)
            {
                string day = DropDownList2.SelectedValue;

                BusLinkedList selectedList = Tasks.FindSelectedCities((BusLinkedList)Session["Bus"],
                    city, departureTime, arrivalTime, day);

                selectedList.Sort();
                selectedList.Begin();
                if (selectedList == null || !selectedList.Exist())
                {
                    Label6.Text = "Tokių maršrutų nėra";
                }
                else
                {
                    Tasks.WriteToTable(selectedList, Table1, (PriceLinkedList)Session["Price"]);
                }

                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "DuomenysIrRezultatai.txt");
                File.AppendAllLines(file, Tasks.ConvertForTxt(selectedList));
            }
            else
            {
                Tasks.WriteToTable((BusLinkedList)Session["Bus"],
                    Table1, (PriceLinkedList)Session["Price"]);
            }

            

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label6.Text = "";
            BusLinkedList selectedList = Tasks.FindMostPopularCityList((BusLinkedList)Session["Bus"]);

            selectedList.DeleteTransits();
            selectedList.Sort();

            Tasks.WriteToTable(selectedList, Table2, (PriceLinkedList)Session["Price"]);

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
                (PriceLinkedList)Session["Price"]));
        }
    }
}