using System;
using System.Text.RegularExpressions;

namespace Serveris
{
    public class Day
    {
        public DateTime Time { get; set; }
        private string ip;
        public string IP
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
        public string Path { get; private set; }
        /// <summary>
        /// Name of the address
        /// </summary>
        public string Name
        {
            get
            {
                return this.Path.Split('.')[1];
            }
        }
        /// <summary>
        /// Spliting full path for treeView
        /// </summary>
        public string[] SplitedPath
        {
            get
            {
                return this.Path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public Day(string iP, string path, DateTime time)
        {
            this.Time = time;
            this.IP = iP;
            this.Path = path;
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
        /// Data line for writing to txt file.
        /// </summary>
        public override string ToString()
        {
            return string.Format("| {0, 10} | {1, 16} | {2} ", this.Time.ToString("T"), this.IP, this.Path);
        }
    }
}