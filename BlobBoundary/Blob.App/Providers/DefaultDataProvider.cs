using System.IO;
using Blob.App.Interfaces;

namespace Blob.App.Providers
{
    class DefaultDataProvider : IDataProvider
    {
        private readonly int[,] Data =
{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0},
            {0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
            {0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
            {0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };

        public DefaultDataProvider()
        {
            ReadsCount = 0;
        }

        public bool Get(int i, int j)
        {
            ReadsCount++;
            return Data[i, j] != 0;
        }

        public int ReadsCount { get; internal set; }
        public int N => 10;
        public void Print(IDataPrinter printer, TextWriter output)
        {
            printer.Print(output, Data);
        }
    }
}