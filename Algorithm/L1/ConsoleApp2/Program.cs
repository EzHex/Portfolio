using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp2
{
    public class Program
    {
        static void Main(string[] args)
        {
            uint Width = 1000;
            //double length = 200;
            uint level = 4;
            //Stopwatch stopwatch = new Stopwatch();

            byte[] Map = Render.CreateMap(Width, Width);

            Render.RecursiveLine(Map, Width/2, Width/2, 800, 0, level);

            //Render.DrawImage(Map, 100, Width / 2, length, level, 1, Width);

            //Console.WriteLine($"{stopwatch.Elapsed.TotalMilliseconds}");

            Render.Export(Map, Width, Width, 0xFFFFFF, 0x000000, "result.bmp");
            
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("result.bmp") { UseShellExecute = true };
            p.Start();
        }



    }
}