using System;

namespace AutobusuStotis
{
    public class Price : IComparable<Price>, IEquatable<Price>
    {
        public string ArrivalCity { get; private set; }
        public decimal TicketPrice { get; private set; }

        public Price(string arrivalCity, decimal ticketPrice)
        {
            this.ArrivalCity = arrivalCity;
            this.TicketPrice = ticketPrice;
        }
        /// <summary>
        /// Creating compareTo methos to sort by rules
        /// First by arrival city in alphabetic order,
        /// then by price
        /// </summary>
        /// <param name="other">Data of other object</param>
        /// <returns>Is current object higher, lower or equal than "other"</returns>
        public int CompareTo(Price other)
        {
            if (other == null) return 1;
            if (ArrivalCity.CompareTo(other.ArrivalCity) != 0)
            {
                return ArrivalCity.CompareTo(other.ArrivalCity);
            }
            else
            {
                return TicketPrice.CompareTo(other.TicketPrice);
            }
        }
        /// <summary>
        /// Checks if current object is equals to "other"
        /// </summary>
        /// <param name="other">Data of other object</param>
        /// <returns>Is current object equal than "other"</returns>
        public bool Equals(Price other)
        {
            if (other == null)
            {
                return false;
            }
            if (ArrivalCity == other.ArrivalCity &&
                TicketPrice == other.TicketPrice)
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
        static public bool operator >(Price lhs, Price rhs)
        {
            return lhs.CompareTo(rhs) == 1;
        }
        static public bool operator <(Price lhs, Price rhs)
        {
            return lhs.CompareTo(rhs) == -1;
        }
        static public bool operator >=(Price lhs, Price rhs)
        {
            return !(lhs < rhs);
        }
        static public bool operator <=(Price lhs, Price rhs)
        {
            return !(lhs > rhs);
        }
    }
}