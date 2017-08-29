using System;

namespace Blob.App
{
    class Program
    {
        private static bool useCache = false;
        private static long N = 10;
        private static long[,] a =
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0},
            {0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
            {0, 0, 1, 0, 0, 0, 1, 0, 0, 0},
            {0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };

        private static long[,] cache =
        {
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        };

        private static long left, right, top, bottom;
        private static long reads = 0;

        static void Main(string[] args)
        {
            PrintArray();

            left = N;
            right = -1;
            top = N;
            bottom = -1;
            long Nm1 = N - 1;

            for (var i = 0; i < N; i++)
            {
                var j = Nm1 - i;
                if (j < i) break;
                long k;
                bool getDiagI = Get(i, i);
                bool getDiagJ = Get(j, j);
                Console.WriteLine(
                    $"LEFT: current value {left}, position {i}, searching from 0 to {Math.Min(i, left)} at row {i}");
                for (k = 0; k < Math.Min(i, left); k++)
                {
                    if (Get(i, k))
                    {
                        Console.WriteLine($"LEFT: found at {k}");
                        left = k;
                        break;
                    }
                }
                if (getDiagI && left > i)
                {
                    left = i;
                    Console.WriteLine($"LEFT: found at diagonal {i}");
                }
                Console.WriteLine();
                Console.WriteLine(
                    $"TOP: current value {top}, position {i}, searching from 0 to {Math.Min(i, top)} at column {i}");
                for (k = 0; k < Math.Min(i, top); k++)
                {
                    if (Get(k, i))
                    {
                        Console.WriteLine($"TOP: found at {k}");
                        top = k;
                        break;
                    }
                }
                if (getDiagI && top > i)
                {
                    top = i;
                    Console.WriteLine($"TOP: found at diagonal {i}");
                }

                Console.WriteLine();
                Console.WriteLine(
                    $"RIGHT: current value {right}, position {j}, searching from {Nm1} to {Math.Max(j, right)} at row {j}");

                for (k = Nm1; k > Math.Max(j, right); k--)
                {
                    if (Get(j, k))
                    {
                        Console.WriteLine($"RIGHT: found at {k}");
                        right = k;
                        break;
                    }
                }
                if (getDiagJ && right < j)
                {
                    right = j;
                    Console.WriteLine($"RIGHT: found at diagonal {j}");
                }
                Console.WriteLine();
                Console.WriteLine(
                    $"BOTTOM: current value {bottom}, position {j}, searching from {Nm1} to {Math.Max(j, bottom)} at column {j}");
                for (k = Nm1; k > Math.Max(j, bottom); k--)
                {
                    if (Get(k, j))
                    {
                        Console.WriteLine($"BOTTOM: found at {k}");
                        bottom = k;
                        break;
                    }
                }
                if (getDiagJ && bottom < j)
                {
                    bottom = j;
                    Console.WriteLine($"Bottom: found at diagonal {j}");
                }
                Console.WriteLine();
                Console.WriteLine("------");
                Console.WriteLine();
            }

            Console.WriteLine($"reads: {reads}");
            Console.WriteLine($"top: {top}");
            Console.WriteLine($"left: {left}");
            Console.WriteLine($"bottom: {bottom}");
            Console.WriteLine($"right: {right}");

            Console.ReadKey();
        }

        private static bool Get(long i, long j)
        {
            if (useCache && cache[i, j] != -1) return cache[i, j] == 1;
            reads++;
            var val = a[i, j];
            cache[i, j] = val;
            return val == 1;
        }

        private static void PrintArray()
        {
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                {
                    Console.Write(a[i, j] == 0 ? "." : "*");
                }
                Console.WriteLine();
            }
        }
    }
}
