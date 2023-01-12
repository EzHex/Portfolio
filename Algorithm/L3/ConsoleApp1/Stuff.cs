using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Stuff
    {
        public string Product { get; set; }
        public int Price { get; set; }
        public int Mass { get; set; }

        public Stuff(string name, int price, int mass)
        {
            Product = name;
            Price = price;
            Mass = mass;
        }

        public Stuff ()
        {

        }

        public override string ToString()
        {
            return $"{this.Product};{this.Price};{this.Mass};";
        }
    }
}
