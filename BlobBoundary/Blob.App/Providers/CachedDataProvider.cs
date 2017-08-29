using System.IO;
using Blob.App.Interfaces;

namespace Blob.App.Providers
{
    public class CachedDataProvider : IDataProvider
    {
        private readonly IDataProvider _baseDataProvider;

        private readonly int[,] Cache =
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

        public CachedDataProvider(IDataProvider baseDataProvider)
        {
            _baseDataProvider = baseDataProvider;
        }

        public bool Get(int i, int j)
        {
            if (Cache[i, j] != -1) return Cache[i, j] == 1;
            var value = _baseDataProvider.Get(i, j);
            Cache[i, j] = value ? 1 : 0;
            return value;
        }

        public int ReadsCount => _baseDataProvider.ReadsCount;
        public int N => _baseDataProvider.N;

        public void Print(IDataPrinter printer, TextWriter output)
        {
            output.WriteLine("Base data");
            _baseDataProvider.Print(printer, output);
            output.WriteLine("\nCache access data");
            printer.Print(output, Cache, -1);

        }
    }
}