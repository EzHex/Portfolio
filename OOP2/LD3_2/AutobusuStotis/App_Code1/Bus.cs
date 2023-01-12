using System;

namespace AutobusuStotis
{
    public class Bus : IComparable<Bus>, IEquatable<Bus>
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
        /// Creating compareTo method for sorting by the rules
        /// First by departure city in alphabetic order,
        /// then by departure time from early to late.
        /// </summary>
        /// <param name="other">Data of other object</param>
        /// <returns>Is current object higher, lower or equal than "other" </returns>
        public int CompareTo(Bus other)
        {
            if (other == null) return 1;
            if (DepartureCity.CompareTo(other.DepartureCity) != 0)
            {
                return DepartureCity.CompareTo(other.DepartureCity);
            }
            else
            {
                return DepartureTime.TimeOfDay.CompareTo(other.DepartureTime.TimeOfDay);
            }
        }
        /// <summary>
        /// Checks if current object is equals to "other"
        /// </summary>
        /// <param name="other">Data of other object</param>
        /// <returns>Is current object equal than "other"</returns>
        public bool Equals(Bus other)
        {
            if (other == null)
            {
                return false;
            }
            if( DepartureCity == other.DepartureCity &&
                DepartureTime.TimeOfDay == other.DepartureTime.TimeOfDay)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Overriding all operators to use for sorting, it uses compareTo method
        /// </summary>
        /// <param name="lhs">Data 1</param>
        /// <param name="rhs">Data 2</param>
        /// <returns>Are left one higher, lower or equal</returns>
        public static bool operator > (Bus lhs, Bus rhs)
        {
            return lhs.CompareTo(rhs) == 1;
        }
        public static bool operator < (Bus lhs, Bus rhs)
        {
            return lhs.CompareTo(rhs) == -1;
        }
        public static bool operator >= (Bus lhs, Bus rhs)   
        { 
            return !(lhs < rhs); 
        }
        public static bool operator <= (Bus lhs, Bus rhs)   
        { 
            return !(lhs > rhs); 
        }

    }
}