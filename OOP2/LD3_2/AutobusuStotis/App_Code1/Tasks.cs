using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace AutobusuStotis
{
    public class Tasks
    {
        /// <summary>
        /// Read from file array to form a Bus linked list
        /// </summary>
        /// <param name="fileA">Array of information from file A</param>
        /// <returns>Bus linked list</returns>
        public static LinkedList<Bus> FormLinkedList(string[] fileA)
        {
            LinkedList<Bus> list = new LinkedList<Bus>(fileA[0]);

            for (int i = 1; i < fileA.Length; i++)
            {
                string[] variables = fileA[i].Split(' ');
                Bus newBus = new Bus(variables[0], DateTime.Parse(variables[1]),
                    variables[2], DateTime.Parse(variables[3]), variables[4]);
                Node<Bus> newNode = new Node<Bus>(newBus, null);

                list.AddNode(newNode);
            }

            return list;
        }
        /// <summary>
        /// Read from file array to form a Price linked list
        /// </summary>
        /// <param name="fileB">Array of information from file B</param>
        /// <returns>Price linked list</returns>
        public static LinkedList<Price> FormPriceList(string[] fileB)
        {
            LinkedList<Price> list = new LinkedList<Price>();

            foreach (string line in fileB)
            {
                string[] variables = line.Split(' ');
                Price newPrice = new Price(variables[0], decimal.Parse(variables[1]));
                Node<Price> newNode = new Node<Price>(newPrice, null);

                list.AddNode(newNode);
            }

            return list;
        }
        /// <summary>
        /// Form a table with the given list and price list
        /// </summary>
        /// <param name="busList">All bus list</param>
        /// <param name="table">Table to write to</param>
        /// <param name="priceList">All price list</param>
        public static void WriteToTable(LinkedList<Bus> busList, Table table, LinkedList<Price> priceList)
        {
            TableRow row1 = new TableRow();

            TableCell departureCity1 = new TableCell();
            departureCity1.Text = "Išvykimo miestas";
            row1.Cells.Add(departureCity1);

            TableCell departureTime1 = new TableCell();
            departureTime1.Text = "Išvykimo laikas";
            row1.Cells.Add(departureTime1);

            TableCell arrivalCity1 = new TableCell();
            arrivalCity1.Text = "Atvykimo miestas";
            row1.Cells.Add(arrivalCity1);

            TableCell arrivalTime1 = new TableCell();
            arrivalTime1.Text = "Atvykimo laikas";
            row1.Cells.Add(arrivalTime1);

            TableCell day1 = new TableCell();
            day1.Text = "Diena";
            row1.Cells.Add(day1);

            TableCell price1 = new TableCell();
            price1.Text = "Kaina";
            row1.Cells.Add(price1);

            table.Rows.Add(row1);

            for (busList.Begin(); busList.Exist(); busList.Next())
            {
                Bus bus = busList.Get();
                TableRow row = new TableRow();

                TableCell departureCity = new TableCell();
                departureCity.Text = bus.DepartureCity;
                row.Cells.Add(departureCity);

                TableCell departureTime = new TableCell();
                departureTime.Text = bus.DepartureTime.ToString("HH:mm");
                row.Cells.Add(departureTime);

                TableCell arrivalCity = new TableCell();
                arrivalCity.Text = bus.ArrivalCity;
                row.Cells.Add(arrivalCity);

                TableCell arrivalTime = new TableCell();
                arrivalTime.Text = bus.ArrivalTime.ToString("HH:mm");
                row.Cells.Add(arrivalTime);

                TableCell day = new TableCell();
                day.Text = bus.Day;
                row.Cells.Add(day);

                TableCell price = new TableCell();
                decimal finalPrice = FindPrice(priceList, bus, busList);
                
                price.Text = string.Format("{0:0.00}", finalPrice);
                row.Cells.Add(price);

                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Finds route price
        /// </summary>
        /// <param name="priceList">All routes prices</param>
        /// <param name="bus">Target route</param>
        /// <param name="busList">All routes</param>
        /// <returns>Price</returns>
        private static decimal FindPrice(LinkedList<Price> priceList, Bus bus, LinkedList<Bus> busList )
        {
            decimal finalPrice = 0;
            for (priceList.Begin(); priceList.Exist(); priceList.Next())
            {
                Price prc = priceList.Get();
                if (prc.ArrivalCity == bus.ArrivalCity)
                {
                    if (bus.DepartureCity != busList.HeadCity)
                    {
                        finalPrice = decimal.Multiply(prc.TicketPrice, 0.9m);
                    }
                    else
                    {
                        finalPrice = prc.TicketPrice;
                    }
                }
            }

            return finalPrice;
        }
        /// <summary>
        /// Finds the routes of all bus routes by the consumer choice
        /// </summary>
        /// <param name="busList">All bus list</param>
        /// <param name="city">Consumer chosen city</param>
        /// <param name="departureTime">Consumer chosen departure time</param>
        /// <param name="arrivalTime">Consumer chosen arrival time</param>
        /// <param name="day">Consumer chosen day</param>
        /// <returns>List of bus routes with consumer criteria</returns>
        public static LinkedList<Bus> FindSelectedCities(LinkedList<Bus> busList, string city,
            DateTime departureTime, DateTime arrivalTime, string day)
        {
            LinkedList<Bus> selectedBusList = new LinkedList<Bus>(busList.HeadCity);

            for (busList.Begin(); busList.Exist(); busList.Next())
            {
                Bus bus = busList.Get();
                if (bus.Day == day)
                {
                    if (bus.ArrivalCity == city)
                    {
                        if (bus.DepartureTime.TimeOfDay >= departureTime.TimeOfDay &&
                              bus.ArrivalTime.TimeOfDay <= arrivalTime.TimeOfDay)
                        {
                            Node<Bus> newBus = new Node<Bus>(bus, null);
                            selectedBusList.AddNode(newBus);
                        }
                    }
                }
            }

            return selectedBusList;
        }
        /// <summary>
        /// Finds all the routes to the most popular city
        /// </summary>
        /// <param name="busList">All bus list</param>
        /// <returns>List of routes to the most popular city</returns>
        public static LinkedList<Bus> FindMostPopularCityList(LinkedList<Bus> busList)
        {
            LinkedList<Bus> selectedBusList = new LinkedList<Bus>(busList.HeadCity);
            string mostPopularCity = FindMostPopularCity(busList);

            for (busList.Begin(); busList.Exist(); busList.Next())
            {
                Bus bus = busList.Get();
                if (bus.ArrivalCity == mostPopularCity)
                {
                    selectedBusList.AddNode(new Node<Bus>(bus, null));
                }
            }

            return selectedBusList;
        }
        /// <summary>
        /// Finding the name of the city with the most routes to it.
        /// </summary>
        /// <param name="busList">All bus list</param>
        /// <returns>Name of the most popular city</returns>
        private static string FindMostPopularCity(LinkedList<Bus> busList)
        {
            string result = "";
            int count = 0;
            List<string> temp = new List<string>();
            LinkedList<Bus> busListCopy = BusListCopy(busList);

            for (busList.Begin(); busList.Exist(); busList.Next())
            {
                Bus bus = busList.Get();
                if (!temp.Contains(bus.ArrivalCity))
                {
                    string targetCity = bus.ArrivalCity;
                    int tempCount = 0;
                    for (busListCopy.Begin();busListCopy.Exist();busListCopy.Next())
                    {
                        if (busListCopy.Get().ArrivalCity == targetCity)
                        {
                            tempCount++;
                        }
                    }

                    if (tempCount > count)
                    {
                        count = tempCount;
                        result = targetCity;
                    }

                    temp.Add(targetCity);
                }
            }

            return result;
        }
        /// <summary>
        /// Makes a deep copy of the BUS list
        /// </summary>
        /// <typeparam name="type">Object type</typeparam>
        /// <param name="busList">Bus list</param>
        /// <returns>Bus list copy</returns>
        private static LinkedList<type> BusListCopy<type>(LinkedList<type> busList) where type :
            IComparable<type>, IEquatable<type>
        {
            LinkedList<type> copy = new LinkedList<type>(busList.HeadCity);

            foreach (type bus in busList)
            {
                copy.AddNode(new Node<type>(bus, null));
            }

            return copy;
        }

        /// <summary>
        /// Checks if it is the word not a number
        /// </summary>
        /// <param name="a">word which needed to be checked</param>
        /// <returns>Is it not number</returns>
        public static bool NotDigit(string a)
        {
            Regex expression = new Regex(@"\d");
            if (expression.IsMatch(a))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks is this a time format
        /// </summary>
        /// <param name="a">string to check</param>
        /// <returns>Is it time format or not</returns>
        public static bool IsItTime (string a)
        {
            Regex expression = new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
            if (expression.IsMatch(a))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Prints data to txt file
        /// </summary>
        public static List<string> ConvertForTxt(LinkedList<Bus> bus, string header)
        {
            List<string> allLines = new List<string>();
            allLines.Add(new string('-', 116));
            allLines.Add(header);
            allLines.Add(new string('-', 116));
            allLines.Add(string.Format("| {0, -20} | {1, -20} | {2, -20} | {3, -20} | {4, -20} |", "Išvykimo miestas",
                "Išvykimo laikas", "Atvykimo miestas", "Atvykimo laikas", "Diena"));
            for (bus.Begin(); bus.Exist(); bus.Next())
            {
                Bus busData = bus.Get();
                allLines.Add(string.Format("| {0, -20} | {1, 20} | {2, -20} | {3, 20} | {4, -20} |", busData.DepartureCity,
                busData.DepartureTime.ToString("HH:mm"), busData.ArrivalCity,
                busData.ArrivalTime.ToString("HH:mm"), busData.Day));
            }

            allLines.Add(new string('-', 116));

            return allLines;
        }
        /// <summary>
        /// Prints data to txt file
        /// </summary>
        public static List<string> ConvertForTxt(LinkedList<Price> price, string header)
        {
            List<string> allLines = new List<string>();
            allLines.Add(new string('-', 47));
            allLines.Add(header);
            allLines.Add(new string('-', 47));
            allLines.Add(string.Format("| {0, -20} | {1, -20} |", "Atvykimo miestas", "Kaina"));
            for (price.Begin(); price.Exist(); price.Next())
            {
                Price priceData = price.Get();
                allLines.Add(string.Format("| {0, -20} | {1, 20} |", priceData.ArrivalCity,
                    priceData.TicketPrice));
            }

            allLines.Add(new string('-', 47));

            return allLines;
        }
        /// <summary>
        /// Prints data to txt file
        /// </summary>
        public static List<string> ConvertForTxt(LinkedList<Bus> bus, LinkedList<Price> price, string header)
        {
            List<string> allLines = new List<string>();
            allLines.Add(new string('-', 139));
            allLines.Add(header);
            allLines.Add(new string('-', 139));
            allLines.Add(string.Format("| {0, -20} | {1, -20} | {2, -20} | {3, -20} | {4, -20} | {5, -20} |",
                "Išvykimo miestas", "Išvykimo laikas", "Atvykimo miestas",
                "Atvykimo laikas", "Diena", "Kaina"));
            for (bus.Begin(); bus.Exist(); bus.Next())
            {
                Bus busData = bus.Get();
                decimal finalPrice = FindPrice(price, busData, bus);
                allLines.Add(string.Format("| {0, -20} | {1, 20} | {2, -20} | {3, 20} | {4, -20} | {5, 20} |",
                    busData.DepartureCity, busData.DepartureTime.ToString("HH:mm"),
                    busData.ArrivalCity, busData.ArrivalTime.ToString("HH:mm"),
                    busData.Day, finalPrice));
            }

            allLines.Add(new string('-', 139));

            return allLines;
        }
        /// <summary>
        /// Makes a new Bus list without transit routes
        /// </summary>
        /// <param name="busList">Bus list</param>
        /// <returns>List without transits</returns>
        public static LinkedList<Bus> DeleteTransits(LinkedList<Bus> busList) 
        {
            LinkedList <Bus> result = new LinkedList<Bus>();
            busList.Begin();
            if (busList.Exist())
            {
                for (busList.Begin();busList.Exist();busList.Next())
                {
                    Bus bus = busList.Get();
                    if (bus.DepartureCity == busList.HeadCity)
                    {
                        result.AddNode(new Node<Bus>(bus, null));
                    }
                }
            }

            return result;
        }
    }
}