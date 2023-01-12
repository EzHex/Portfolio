using System;
using System.Collections.Generic;
using System.IO;

namespace AutomobiliuParkas
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<TransportRegister> registers = (List<TransportRegister>)Session["register"];
            if (registers != null) Task.WriteToTable(registers, Table1);

            List<Transport> bestTransports = (List<Transport>)Session["bestTransports"];
            if (bestTransports != null) Task.WriteToTableForTheBestTransports(bestTransports, "Geriausios transporto priemonės", Table2);

            List<TransportRegister> trucks = (List<TransportRegister>)Session["trucks"];
            if (trucks != null) Task.WriteToTable(trucks, Table3);

            List<Transport> transports = (List<Transport>)Session["transport"];
            if (transports != null) Task.WriteToTableForTechnicalInspection(transports, "Reikalinga techninė apžiūra", Table4);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label2.Visible = false;
            Label3.Text = "";
            Label4.Visible = false;

            try
            {
                var dir = Server.MapPath("~/App_Data");
                string[] txtFiles = Directory.GetFiles(dir, "duom*.txt");

                List<TransportRegister> registers = InOut.ReadFiles(txtFiles);

                var file = Path.Combine(dir, "Rezultatai.txt");
                Directory.CreateDirectory(dir);
                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                Task.WriteToTable(registers, Table1);
                File.AppendAllLines(file, Task.ConvertForTxtAll(registers, "Pradiniai duomenys"));

                Session["register"] = registers;
                Button1.Visible = false;
                Button2.Visible = true;

            }
            catch (Exception)
            {
                Label3.Text = "Nėra aplankalo ar duomenų failų arba bent viename iš duomenų failų yra klaida";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label3.Text = "";
            try
            {
                List<TransportRegister> registers = (List<TransportRegister>)Session["register"];
                List<Transport> bestTransports = Task.FindBestTransportsInEachCategory(registers);
                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "Rezultatai.txt");
                Session["bestTransports"] = bestTransports;
                Task.WriteToTableForTheBestTransports(bestTransports, "Geriausios transporto priemonės", Table2);
                File.AppendAllLines(file, Task.ConvertForTxt(bestTransports, "Geriausios transporto priemonės"));
                Button2.Visible = false;
                Button3.Visible = true;
            }
            catch (Exception)
            {
                Label3.Text = "Nepavyko surasti geriausių transporto priemonių.";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label3.Text = "";
            try
            {
                List<TransportRegister> registers = (List<TransportRegister>)Session["register"];
                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "Rezultatai.txt");
                List<string> branch = Task.FindBranchWithOldestMicrobuses(registers);
                Label2.Text = branch[3].Trim('|') + ", " + branch[4].Trim('|') + ", " + branch[5].Trim('|');
                File.AppendAllLines(file, branch);
                Label2.Visible = true;
                Label4.Visible = true;
                Button3.Visible = false;
                Button4.Visible = true;
            }
            catch (Exception)
            {
                Label3.Text = "Nepavyko rasti kuriame filiale yra seniausi microautobusai";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Label3.Text = "";
            try
            {
                List<TransportRegister> registers = (List<TransportRegister>)Session["register"];
                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "Rezultatai.txt");
                List<TransportRegister> trucks = Task.FormEveryBranchTrucksOnlyList(registers);
                Session["trucks"] = trucks;
                //This foreach is needed to sort every truck register.
                foreach (TransportRegister truck in trucks)
                {
                    truck.Sort();
                    if (truck.Count() < 1)
                    {
                        List<string> temp = new List<string>();
                        temp.Add("Iš " + truck.City + ", " + truck.Address + ", " + truck.Email + ". Nėra krovininių automobilių");
                        File.AppendAllLines(file, temp);
                    }
                    else File.AppendAllLines(file, Task.ConvertForTxt(truck, "Surušiuoti krovininiai automobiliai"));
                }
                Task.WriteToTable(trucks, Table3);
                Button4.Visible = false;
                Button5.Visible = true;
            }
            catch (Exception)
            {
                Label3.Text = "Nepavyko atrinkti krovininių automobilių";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Label3.Text = "";
            try
            {
                List<TransportRegister> registers = (List<TransportRegister>)Session["register"];
                var dir = Server.MapPath("~/App_Data");
                var file = Path.Combine(dir, "Apžiūra.txt");
                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                List<Transport> transports = Task.FormInspectionNeededTransportsList(registers);
                Session["transport"] = transports;
                Task.WriteToTableForTechnicalInspection(transports, "Reikalinga techninė apžiūra", Table4);
                File.AppendAllLines(file, Task.ConvertForTxtTechnicalInspection(transports, "Reikalinga techninė apžiūra"));
                Button5.Visible = false;
            }
            catch (Exception)
            {
                Label3.Text = "Nepavyko nustatyti techninės apžiūros datų.";
            }
        }
    }
}