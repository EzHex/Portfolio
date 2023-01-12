using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Serveris
{
    public class DayRegister : IEnumerable<Day>
    {
        public DateTime Date { get; private set; }
        private string ip;
        public string ServerIP
        {
            get
            {
                return ip;
            }
            private set
            {
                if (!IsIPValid(value))
                {
                    throw new ArgumentException(string.Format("IP adresas {0} yra blogo formato", value));
                }
                this.ip = value;
            }
        }
        private List<Day> allDays;

        public DayRegister(DateTime date, List<Day> allDays, string ip)
        {
            this.Date = date;
            this.allDays = allDays;
            this.ServerIP = ip;
        }
        /// <summary>
        /// Returns count of data in allDays list.
        /// </summary>
        public int Count()
        {
            return allDays.Count;
        }
        /// <summary>
        /// Returns data from list by index
        /// </summary>
        public Day GetDay(int index)
        {
            return allDays[index];
        }
        /// <summary>
        /// Checks if given string is valid ip address
        /// </summary>
        private bool IsIPValid(string ip)
        {
            Regex expression = new Regex(@"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
            return expression.IsMatch(ip);
        }
        /// <summary>
        /// Foreach loop
        /// </summary>
        public IEnumerator<Day> GetEnumerator()
        {
            //for (int i = 0; i < Count(); i++)
            //{
            //    yield return allDays[i];
            //}
            return allDays.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Data line for writing to txt file.
        /// </summary>
        public override string ToString()
        {
            return string.Format("| {0,10} | {1, 16} |", this.Date.ToString("yyyy-MM-dd"), this.ServerIP);
        }
    }
}