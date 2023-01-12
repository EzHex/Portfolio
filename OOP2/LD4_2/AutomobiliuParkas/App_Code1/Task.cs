using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace AutomobiliuParkas
{
    public class Task
    {
        /// <summary>
        /// Writing registers info to table
        /// </summary>
        public static void WriteToTable(List<TransportRegister> registers, Table table)
        {
            foreach (TransportRegister register in registers)
            {

                TableRow row = new TableRow();
                TableCell cell = new TableCell();

                cell.Text = register.City;
                cell.ColumnSpan = 2;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = register.Address;
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = register.Email;
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                row.CssClass = "preFirstRow";
                table.Rows.Add(row);

                if (register.Count() > 0)
                {
                    table.Rows.Add(TableHeaderRow());

                    for (int i = 0; i < register.Count(); i++)
                    {
                        Transport transport = register.GetTransport(i);
                        row = new TableRow();
                        cell = new TableCell();
                        cell.Text = transport.LicensePlate;
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = transport.Manufactor;
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = transport.Model;
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = transport.YearOfManufaction.ToString("yyyy-MM-dd");
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = transport.TechnicalInspection.ToString("yyyy-MM-dd");
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = transport.Fuel;
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = transport.AverageConsumption.ToString();
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = transport.Feature().ToString().TrimEnd('|');
                        row.Cells.Add(cell);

                        table.Rows.Add(row);
                    }
                }
                else
                {
                    row = new TableRow();
                    cell = new TableCell();

                    cell.Text = "Nėra krovininių automobilių";
                    cell.ColumnSpan = 8;
                    row.Cells.Add(cell);

                    table.Rows.Add(row);
                }
            }
        }
        /// <summary>
        /// Writes info to table by the rules
        /// </summary>
        public static void WriteToTableForTheBestTransports(List<Transport> list, string header, Table table)
        {
            TableCell cell = new TableCell();
            TableRow row = new TableRow();
            cell.Text = header;
            cell.ColumnSpan = 5;
            row.Cells.Add(cell);
            row.CssClass = "preFirstRow";
            table.Rows.Add(row);
            table.Rows.Add(TableHeaderRowForTheBestTransports());
            foreach (Transport transport in list)
            {
                row = new TableRow();
                cell = new TableCell();

                cell.Text = transport.Manufactor;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = transport.Model;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = transport.LicensePlate;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = transport.Age.ToString();
                row.Cells.Add(cell);

                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Writes info to table by the rules
        /// </summary>
        public static void WriteToTableForTechnicalInspection(List<Transport> list, string header, Table table)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            cell.Text = header;
            cell.ColumnSpan = 4;
            row.Cells.Add(cell);
            row.CssClass = "preFirstRow";
            table.Rows.Add(row);

            row = new TableRow();
            cell = new TableCell();

            cell.Text = "* pažymėtos datos reiškia, jog techninė apžiūra jau pasibaigusi";
            cell.ColumnSpan = 4;
            row.Cells.Add(cell);
            row.CssClass = "preFirstRow";
            table.Rows.Add(row);

            table.Rows.Add(TableHeaderRowForTechnicalInspection());

            foreach (Transport transport in list)
            {
                row = new TableRow();
                cell = new TableCell();

                cell.Text = transport.Manufactor;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = transport.Model;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = transport.LicensePlate;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = transport.EndOfTechnicalInspection().Trim('|');
                row.Cells.Add(cell);

                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Header row for table
        /// </summary>
        private static TableHeaderRow TableHeaderRowForTheBestTransports()
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow();
            TableHeaderCell tableHeaderCell = new TableHeaderCell();

            tableHeaderCell.Text = "Gamintojas";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Modelis";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Valstybinis numeris";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Amžius";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderRow.CssClass = "firstrow";

            return tableHeaderRow;
        }
        /// <summary>
        /// Header row for table
        /// </summary>
        private static TableHeaderRow TableHeaderRow()
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow();
            TableHeaderCell tableHeaderCell = new TableHeaderCell();

            tableHeaderCell.Text = "Valstybinis numeris";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Gamintojas";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Modelis";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Pagaminimo  metai  ir  mėnuo";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Atliktos techninės apžiūros data";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Kuras";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Vidutinės kuro sąnaudos";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Savybė";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderRow.CssClass = "firstrow";

            return tableHeaderRow;
        }
        /// <summary>
        /// Header row for table
        /// </summary>
        private static TableHeaderRow TableHeaderRowForTechnicalInspection()
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow();
            TableHeaderCell tableHeaderCell = new TableHeaderCell();

            tableHeaderCell.Text = "Gamintojas";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Modelis";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Valstybinis numeris";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.Text = "Apžiūros pabaigos laikas";
            tableHeaderRow.Cells.Add(tableHeaderCell);

            tableHeaderRow.CssClass = "firstrow";

            return tableHeaderRow;
        }
        /// <summary>
        /// Converting registers for writing to txt file
        /// </summary>
        public static List<string> ConvertForTxtAll(List<TransportRegister> registers, string header)
        {
            List<string> allLines = new List<string>();
            foreach (TransportRegister register in registers)
            {
                allLines.AddRange(ConvertForTxt(register, header));
            }
            return allLines;
        }
        /// <summary>
        /// Converting register for writing to txt file
        /// </summary>
        public static List<string> ConvertForTxt(TransportRegister register, string header)
        {
            List<string> allLines = new List<string>();
            allLines.Add(Separator());
            allLines.Add(header);
            allLines.Add(Separator());
            allLines.Add(register.City);
            allLines.Add(register.Address);
            allLines.Add(register.Email);
            allLines.Add(Separator());
            allLines.Add(Title());
            allLines.Add(Separator());

            for (int i = 0; i < register.Count(); i++)
            {
                Transport transport = register.GetTransport(i);
                allLines.Add(transport.ToString() + transport.Feature());
            }
            allLines.Add(Separator());

            return allLines;
        }
        /// <summary>
        /// Converting list for writing to txt file
        /// </summary>
        public static List<string> ConvertForTxt(List<Transport> list, string header)
        {
            List<string> allLines = new List<string>();
            allLines.Add(new string('-', 61));
            allLines.Add(header);
            allLines.Add(new string('-', 61));
            allLines.Add(BestTransportsTitle());
            allLines.Add(new string('-', 61));
            foreach (Transport transport in list)
            {
                allLines.Add(transport.ToStringForBestTransports());
            }
            allLines.Add(new string('-', 61));

            return allLines;
        }
        /// <summary>
        /// Converting list for writing to txt file by rules
        /// </summary>
        public static List<string> ConvertForTxtTechnicalInspection(List<Transport> list, string header)
        {
            List<string> allLines = new List<string>();
            allLines.Add(new string('-', 78));
            allLines.Add(header);
            allLines.Add(new string('-', 78));
            allLines.Add(TechnicalInspectionTitle());
            allLines.Add(new string('-', 78));
            foreach (Transport transport in list)
            {
                allLines.Add(transport.ToStringForTechnicalInspection() + transport.EndOfTechnicalInspection());
            }
            allLines.Add(new string('-', 78));

            return allLines;
        }
        /// <summary>
        /// Table lines to write in txt
        /// </summary>
        private static string Separator()
        {
            return new string('-', 182);
        }
        /// <summary>
        /// Title text for txt file
        /// </summary>
        private static string Title()
        {
            return string.Format("| {0,-20} | {1,-10} | {2,-10} | {3,-30} | {4,-35} | {5,-10} | {6,-25} | {7,-17} |",
                "Valstybinis  numeris", "Gamintojas", "Modelis", "Pagaminimo  metai  ir  mėnuo",
                "Atliktos techninės apžiūros data", "Kuras", "Vidutinės kuro sąnaudos", "Savybė");
        }
        /// <summary>
        /// Title text for txt file by best trasnsport rules
        /// </summary>
        private static string BestTransportsTitle()
        {
            return string.Format("| {0,-10} | {1,-10} | {2,-20} | {3, -8} |", "Gamintojas", "Modelis",
                "Valsybinis numeris", "Amžius");
        }
        /// <summary>
        /// Title text for txt file by technical inspection rules
        /// </summary>
        private static string TechnicalInspectionTitle()
        {
            return string.Format("| {0,-10} | {1,-10} | {2,-20} | {3, -25} |", "Gamintojas", "Modelis",
                "Valsybinis numeris", "Apžiūros pabaigos laikas");
        }
        /// <summary>
        /// Finding best transports in each category by the given rules
        /// </summary>
        public static List<Transport> FindBestTransportsInEachCategory(List<TransportRegister> registers)
        {
            List<Transport> transports = new List<Transport>();
            Car bestCar = new Car(null, null, null, default(DateTime), default(DateTime), null, default(double));
            Truck bestTruck = new Truck(null, null, null, default(DateTime), default(DateTime), null, default(double));
            Microbus bestMicrobus = new Microbus(null, null, null, default(DateTime), default(DateTime), null, default(double));

            try
            {
                foreach (TransportRegister register in registers)
                {
                    for (int i = 0; i < register.Count(); i++)
                    {
                        Transport transport = register.GetTransport(i);
                        if (transport is Car)
                        {
                            if (((Car)transport).Odometer < bestCar.Odometer)
                            {
                                bestCar = (Car)transport;
                            }
                        }
                        else if (transport is Truck)
                        {
                            if (((Truck)transport).Volume > bestTruck.Volume)
                            {
                                bestTruck = (Truck)transport;
                            }
                        }
                        else if (transport is Microbus)
                        {
                            if (((Microbus)transport).Seats > bestMicrobus.Seats)
                            {
                                bestMicrobus = (Microbus)transport;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Registro nėra");
            }

            if (bestCar.LicensePlate != null) transports.Add(bestCar);
            if (bestTruck.LicensePlate != null) transports.Add(bestTruck);
            if (bestMicrobus.LicensePlate != null) transports.Add(bestMicrobus);

            return transports;
        }
        /// <summary>
        /// Finding branch with average oldest microbuses
        /// </summary>
        public static List<string> FindBranchWithOldestMicrobuses(List<TransportRegister> registers)
        {
            List<string> allLines = new List<string>();
            int count = 0;
            int i = 0;
            double max = 0;

            foreach (TransportRegister register in registers)
            {
                double temp = AverageAgeOfMicrobuses(register);
                if (temp > max)
                {
                    max = temp;
                    count = i;
                }
                i++;
            }

            allLines.Add(new string('-', 54));
            allLines.Add("Filialas su seniausiais microautobusais");
            allLines.Add(new string('-', 54));
            allLines.Add(string.Format("| {0,-50} |", registers[count].City));
            allLines.Add(string.Format("| {0,-50} |", registers[count].Address));
            allLines.Add(string.Format("| {0,-50} |", registers[count].Email));
            allLines.Add(new string('-', 54));

            return allLines;
        }
        /// <summary>
        /// Method to calculate average age of microbuses
        /// </summary>
        private static double AverageAgeOfMicrobuses(TransportRegister register)
        {
            double average = 0;
            int count = 0;

            for (int i = 0; i < register.Count(); i++)
            {
                Transport transport = register.GetTransport(i);
                if (transport is Microbus)
                {
                    count++;
                    average = average + transport.Age;
                }
            }
            return average / count;
        }
        /// <summary>
        /// Forming truck list from register
        /// </summary>
        public static List<TransportRegister> FormEveryBranchTrucksOnlyList(List<TransportRegister> registers)
        {
            List<TransportRegister> result = new List<TransportRegister>();

            foreach (TransportRegister register in registers)
            {
                List<Transport> transports = new List<Transport>();

                for (int i = 0; i < register.Count(); i++)
                {
                    Transport transport = register.GetTransport(i);
                    if (transport is Truck)
                    {
                        transports.Add(transport);
                    }
                }

                TransportRegister truckRegister = new TransportRegister(register.City, register.Address, register.Email,
                    transports);

                result.Add(truckRegister);
            }

            return result;
        }
        /// <summary>
        /// Forming list of vechicles with inspection needed from all registers
        /// </summary>
        public static List<Transport> FormInspectionNeededTransportsList(List<TransportRegister> registers)
        {
            List<Transport> transports = new List<Transport>();

            foreach (TransportRegister register in registers)
            {
                for (int i = 0; i < register.Count(); i++)
                {
                    Transport transport = register.GetTransport(i);
                    TimeSpan time = transport.TimeForTechnicalInspection;
                    if (time < TimeSpan.Parse("30"))
                    {
                        transports.Add(transport);
                    }
                }
            }

            return transports;
        }
    }
}