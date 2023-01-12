using System;

namespace AutobusuStotis
{
    public class Bus
    {
        public string DepartureCity { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public string ArrivalCity { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public string Day { get; private set; }

        public Bus(string departureCity, DateTime departureTime,
            string arrivalCity, DateTime arrivalTime, string day)
        {
            this.DepartureCity = departureCity;
            this.DepartureTime = departureTime;
            this.ArrivalCity = arrivalCity;
            this.ArrivalTime = arrivalTime;
            this.Day = day;
        }
        /// <summary>
        /// Overriding operator ">" and "<" for sorting by the rules
        /// First by departure city in alphabetic order,
        /// then by departure time from early to late.
        /// </summary>
        /// <param name="a">Data a</param>
        /// <param name="b">Data b</param>
        /// <returns>Is "a" must be higher or not </returns>
        public static bool operator >(Bus a, Bus b)
        {
            if (a.DepartureCity != b.DepartureCity)
            {
                int ip = String.Compare(a.DepartureCity, b.DepartureCity, StringComparison.CurrentCulture);
                return a.DepartureCity.Length > b.DepartureCity.Length ||
                    a.DepartureCity.Length == b.DepartureCity.Length && ip > 0;
            }
            if (a.DepartureTime.TimeOfDay != b.DepartureTime.TimeOfDay)
            {
                return a.DepartureTime.TimeOfDay > b.DepartureTime.TimeOfDay;
            }

            return false;
        }
        public static bool operator <(Bus a, Bus b)
        {
            return !(a > b);
        }

    }
}