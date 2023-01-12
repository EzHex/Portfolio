using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            long count = 10;
            

            for (long i = 0; i < 6; i++)
            {
                long k = 0;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Fun3(count, ref k);
                stopwatch.Stop();

                Console.WriteLine($"{i}: {count}, {k} | {stopwatch.Elapsed.TotalMilliseconds}");

                count += 20;
                GC.Collect();
            }


            Console.ReadKey();
        }

        private static void Fun1(long n, ref long k)
        {
            k++;                                    
            if (n < 5) return;                      

            k += 2;                                 
            Fun1(n / 5, ref k);                     
            Fun1(n / 5, ref k);                     
        }
        private static void Fun1(long n)
        {                                 
            if (n < 5) return;               // c1, c2 | 1
                                
            Fun1(n / 5);                     // T( n / 5 ) | 1
            Fun1(n / 5);                     // T( n / 5 ) | 1
        }

        private static void Fun2(long n, ref long k)
        {
            k++;
            if (n < 6) return;
            k += 2;
            Fun2(n / 5, ref k);
            Fun2(n / 6, ref k);
            for(long i = 0; i < n; i++)
            {
                k++;
                for (long i2 = 0; i2 < n; i2++)
                {
                    k++;
                }
                k++;
            }
            k++;
            
        }
        
        private static void Fun2(long n)
        {
            if (n < 6) return;                      // c1, c2 | 1
            long temp = 0;                          // c3 | 1
            Fun2(n / 5);                            // T(n / 5) | 1
            Fun2(n / 6);                            // T(n / 6) | 1
            for (long i = 0; i < n; i++)            // c4 | n + 1
            {
                for (long i2 = 0; i2 < n; i2++)     // c5 | n(n + 1)
                {
                    temp++;                         // c3 | n^2
                }
            }
        }

        private static void Fun3(long n, ref long k)
        {
            k++;
            if (n < 10) return;

            k++;
            long temp = 0;

            k += 2;
            Fun3(n - 10, ref k);
            Fun3(n - 3, ref k);

            k++;
            for (long i = 0; i < n; i++)
            {
                k++;
                temp++;
            }
            k++;
        }
        
        private static void Fun3(long n)
        {
            if (n < 10) return;             // c1, c2 | 1

            long temp = 0;                  // c3 | 1

            Fun3(n - 10);                   // T(n - 10) | 1
            Fun3(n - 3);                    // T(n - 3) | 1

            for (long i = 0; i < n; i++)    // c4 | n + 1
            {
                temp++;                     // c3 | n
            }
        }
    }
}
