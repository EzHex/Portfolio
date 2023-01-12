using System.Collections.Generic;
using System.Linq;

namespace AutomobiliuParkas
{
    public class TransportRegister
    {
        public string City { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }

        private List<Transport> AllTransports;

        public TransportRegister(string city, string address, string email, List<Transport> allTransports)
        {
            this.City = city;
            this.Address = address;
            this.Email = email;
            this.AllTransports = allTransports;
        }
        /// <summary>
        /// Returns the count of transports in list
        /// </summary>
        public int Count()
        {
            return AllTransports.Count();
        }
        /// <summary>
        /// returns data of Transport
        /// </summary>
        /// <param name="index">index in list</param>
        public Transport GetTransport(int index)
        {
            return AllTransports[index];
        }
        /// <summary>
        /// Bubble sort
        /// </summary>
        public void Sort()
        {
            Transport change;
            bool flag = true;
            while (flag == true)
            {
                flag = false;
                for (int i = 0; i < Count() - 1; i++)
                {
                    if (AllTransports[i].CompareTo(AllTransports[i + 1]) == 1)
                    {
                        flag = true;
                        change = AllTransports[i];
                        AllTransports[i] = AllTransports[i + 1];
                        AllTransports[i + 1] = change;
                    }
                }
            }
        }
    }
}