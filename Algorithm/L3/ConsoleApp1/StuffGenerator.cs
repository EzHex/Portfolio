using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bogus;

namespace ConsoleApp1
{
    public class StuffGenerator
    {
        public static void Generate(int count)
        {
            var testStuff = new Faker<Stuff>()
                //.RuleFor(u => u.Product, (f, u) => f.Commerce.Product())
                .RuleFor(u => u.Product, (f, u) => f.Commerce.ProductName())
                .RuleFor(u => u.Price, (f, u) => f.Random.Int(10, 100))
                .RuleFor(u => u.Mass, (f, u) => f.Random.Int(1, 50));
                //.FinishWith((f, u) =>
                //{
                //    Console.WriteLine(u.ToString());
                //});

            var a = testStuff.Generate(count);

            List<string> list = new List<string>();

            foreach (var item in a)
            {
                list.Add(item.ToString());
            }

            File.WriteAllLines("items.csv", list);
        }

        public static List<Stuff> ConvertFromCsv(string[] lines)
        {
            List<Stuff> list = new List<Stuff>();
            foreach (var item in lines)
            {
                var line = item.Split(';');
                Stuff stuff = new Stuff(line[0], int.Parse(line[1]), int.Parse(line[2]));
                list.Add(stuff);
            }

            return list;
        }

    }
}
