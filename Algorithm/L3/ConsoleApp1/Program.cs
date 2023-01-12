using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long[] p = { 7, 5, 8, 9, 5, 2, 5, 2, 9, 180, 12, 135, 125 };
            long k = 13;
            long n = p.LongLength;

            Console.WriteLine("------------------");
            Console.WriteLine("Pirma užduotis");
            Console.WriteLine("------------------");
            Fun1(k, n, p);



            //int s = 30;
            //Console.WriteLine("------------------");
            //Console.WriteLine("Antra užduotis");
            //Console.WriteLine("------------------");
            //var watch = Stopwatch.StartNew();
            //Console.WriteLine($"Number of ways ({s}) = {Fun2(s)}");
            //watch.Stop();
            //Console.WriteLine($"time: {watch.ElapsedTicks}");
            //watch = Stopwatch.StartNew();
            //Console.WriteLine($"Number of ways rec ({s}) = {Fun2Rec(s)}");
            //watch.Stop();
            //Console.WriteLine($"time: {watch.ElapsedTicks}");
            //Console.WriteLine("------------------");


            //Console.WriteLine("------------------");
            //Console.WriteLine("Trečia užduotis");
            //Console.WriteLine("------------------");

            ////StuffGenerator.Generate(100);

            //var lines = File.ReadAllLines("items.csv");
            //var list = StuffGenerator.ConvertFromCsv(lines);

            //Fun3(1000, 10, list);
            //Console.WriteLine("------------------");


            Console.ReadKey();
        }

        private static void Print(long k, long n, long[,] a)
        {
            for (int i = 0; i < k; i++)
            {
                for (int i2 = 0; i2 < n; i2++)
                {
                    Console.Write($"{a[i, i2], 3} ");
                }
                Console.WriteLine();
            }
        }
        public static long[,] Fun1(long k, long n, long[] p)
        {
            long[,] a = new long[k,n];

            var watch = Stopwatch.StartNew();

            for (long i = k - 1; i >= 0; i--)
            {
                for (long i2 = n - 1; i2 >= 0; i2--)
                {
                    a[i, i2] = Fun1Recursion(i, i2, p);
                }
            }

            watch.Stop();

            Console.WriteLine("------------------");
            Console.WriteLine("Rekursinis");
            Console.WriteLine("------------------");
            Print(k, n, a);
            Console.WriteLine($"time: {watch.ElapsedMilliseconds}");

            Console.WriteLine("------------------");
            Console.WriteLine("Dinaminis");
            Console.WriteLine("------------------");
            watch = Stopwatch.StartNew();
            Print(k, n, Fun1Dynamic(k, n, p));
            watch.Stop();
            Console.WriteLine($"time: {watch.ElapsedMilliseconds}");

            Console.WriteLine("------------------");

            return a;

            long Fun1Recursion(long k1, long n1, long[] p1)
            {
                if (n1 != 0)                                                // c1 | 1
                {
                    if (k1 == 0)                                            // c1 | 1
                    {
                        long temp = 0;                                      // c2 | 1
                        for (int i3 = 0; i3 < n1; i3++)                     // c3 | (n1 + 1)
                        {
                            temp += p1[i3];                                 // c4 | n1
                        }
                        return temp;                                        // c5 | 1
                    }
                    else
                    {
                        long min = long.MaxValue;                           // c2 | 1
                        for (int l = 0; l < n1; l++)                        // c3 | (n1 + 1)
                        {
                            long temp = Math.Max(p1[l], Fun1Recursion(k1 - 1, n1 - 1, p1)); // c6 | n1 * Fun1Recursion(k-1, n-1)
                            if (temp < min)                                 // c1 | n1
                            {
                                min = temp;                                 // c2 | n1
                            }
                        }
                        return min;                                         // c5 | 1
                    }
                }

                return 0;                                                   // c5 | 1
            }

            long[,] Fun1Dynamic(long k1, long n1, long[] p1)
            {
                long[,] result = new long[k1, n1];                          // c1 | 1

                for (int i = 0; i < k1; i++)                                // c2 | k1 + 1
                {
                    for (int i2 = 0; i2 < n1; i2++)                         // c3 | k1(n1+1)
                    {
                        if (i2 != 0)                                        // c4 | k1*n1
                        {
                            if (i == 0)                                     // c5 | k1*n1
                            {
                                long temp = 0;                              // c6 | k1*n1
                                for (int i3 = 0; i3 < i2; i3++)             // c7 | k1*n1*(i2+1)
                                {
                                    temp += p1[i3];                         // c8 | k1*n1*i2
                                }
                                result[i, i2] = temp;                       // c6 | k1*n1
                                continue;                                   // c9 | k1*n1
                            }
                            else
                            {
                                long min = long.MaxValue;                   // c6 | k1*n1
                                for (int l = 0; l < i2; l++)                // c7 | k1*n1*(i2+1)
                                {
                                    long temp = Math.Max(p1[l], result[i-1,i2-1]); // c10 | k1*n1*i2
                                    if (temp < min)                         // c11 | k1*n1*i2
                                    {
                                        min = temp;                         // c6 | k1*n1*i2
                                    }
                                }
                                result[i, i2] = min;                        // c6 | k1*n1
                                continue;                                   // c9 | k1*n1
                            }
                        }
                        result[i, i2] = 0;                                  // c6 | k1*n1
                    }

                }

                return result;                                              // c12 | 1
            }
        }

        


        public static int Fun2(int n)
        {
            int[] res = new int[n + 2];             // c1 | 1
            res[0] = 1;                             // c2 | 1
            res[1] = 1;                             // c2 | 1
            res[2] = 2;                             // c2 | 1

            for (int i = 3; i <= n; i++)            // c3 | n-3+1
                res[i] = res[i - 1] + res[i - 3];   // c2 | n-3

            return res[n-1];                        // c4 | 1
        }

        public static int Fun2Rec(int n)
        {
            if (n == 0)                                 // c1 | 1
                return 1;                               // c2 | 1
            else if (n < 0)                             // c1 | 1
                return 0;                               // c2 | 1
            else
                return Fun2Rec(n - 3) + Fun2Rec(n - 1); // c3 | Fun2Rec(n-3) + Fun2Rec(n-1)
        }

        public static void Fun3(int W, int n, List<Stuff> m)
        {
            var watch = Stopwatch.StartNew();
            Console.WriteLine($"rekursinis = {knapSackRec(W, m, n)}");
            watch.Stop();
            Console.WriteLine($"time: {watch.ElapsedTicks}");
            watch = Stopwatch.StartNew();
            Console.WriteLine($"dinaminis = {knapSack(W, m, n)}");
            watch.Stop();
            Console.WriteLine($"time: {watch.ElapsedTicks}");
        }

        static decimal knapSackRec(int W, List<Stuff> items, int n)
        {

            if (n == 0 || W == 0)   // c1 | 1
                return 0;           // c2 |  1

            // If weight of the nth item is
            // more than Knapsack capacity W,
            // then this item cannot be
            // included in the optimal solution
            if (items[n - 1].Mass > W)  // c1 | 1
                return knapSackRec(W, items, n - 1);    // c2 | knapSackRec(n-1)

            // Return the maximum of two cases:
            // (1) nth item included
            // (2) not included
            else
                return Math.Max(items[n - 1].Price + knapSackRec(W - items[n - 1].Mass, items, n - 1),
                    knapSackRec(W, items, n - 1));  // c3 | knapSackRec(n-1) * knapSackRec(n-1)
        }

        static int knapSack(int W, List<Stuff> items, int n)
        {
            int i, w;                           // c1 | 1
            int[,] K = new int[n + 1, W + 1];   // c1 | 1

            // Build table K[][] in bottom
            // up manner
            for (i = 0; i <= n; i++)            // c2 | n+1
            {
                for (w = 0; w <= W; w++)        // c3 | n(w+1) 
                {
                    if (i == 0 || w == 0)       // c4 | n*w
                        K[i, w] = 0;            // c5 | n*w
                    else if (items[i - 1].Mass <= w)    // c4 | n*w
                        K[i, w] = Math.Max(items[i - 1].Price + K[i - 1, w - items[i - 1].Mass], K[i - 1, w]);  // c5 | n*w
                    else
                        K[i, w] = K[i - 1, w];  // c5 | n*w
                }
            }

            return K[n, W]; // c6 | 1
        }
    }
}
