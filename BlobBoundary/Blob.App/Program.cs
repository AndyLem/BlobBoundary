using System;
using Blob.App.Detectors;
using Blob.App.Interfaces;
using Blob.App.Models;
using Blob.App.Providers;
using Blob.App.Services;
using log4net;
using log4net.Config;

namespace Blob.App
{
    class Program
    {
        private static ILog _log = LogManager.GetLogger("Blob.App");


        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            

            IFactory<IBoundaryDetector> detectorsFactory = new Factory<IBoundaryDetector>(
                new CornerBoundaryDetector(),
                new DiagonalBoundaryDetector(),
                new RecursiveBoundaryDetector());

            IDataPrinter printer = new DataPrinter();
            int option;
            do
            {
                Console.WriteLine("\n1: Corner algorithm");
                Console.WriteLine("2: Diagonal algorithm");
                Console.WriteLine("3: Recursive algorithm");
                Console.WriteLine("------");
                Console.WriteLine("0: Quit");
                var input = Console.ReadLine();
                if (!int.TryParse(input, out option))
                {
                    option = -1;
                    continue;
                }
                if (option == 0) return;
                if (option > 3 || option < 1) continue;

                var detector = detectorsFactory.Get(option - 1);
                Console.Clear();

                Console.WriteLine($"Selected algorithm is {detector.GetType()}");

                IDataProvider rawData = new DefaultDataProvider();
                IDataProvider cache = new CachedDataProvider(rawData);

                var result = detector.DetectBoundary(cache);

                cache.Print(printer, Console.Out);

                detector.PrintAdditionalInfo(printer, Console.Out);

                PrintResult(result, rawData.ReadsCount);

            } while (option != 0);
        }

        private static void PrintResult(Boundary result, int readsCount)
        {
            Console.WriteLine($"\nCell Reads: {readsCount}");
            Console.WriteLine($"Top: {result.Top}");
            Console.WriteLine($"Left: {result.Left}");
            Console.WriteLine($"Bottom: {result.Bottom}");
            Console.WriteLine($"Right: {result.Right}");
        }
    }
}
