using System.IO;
using Blob.App.Interfaces;

namespace Blob.App.Services
{
    class DataPrinter : IDataPrinter
    {
        public void Print(TextWriter output, int[,] data, int defaultEmptyValue = 0, bool printRawNonEmptyValues = false)
        {
            for (var i = 0; i < data.GetLength(0); i++)
            {
                for (var j = 0; j < data.GetLength(1); j++)
                {
                    output.Write(data[i, j] == defaultEmptyValue
                        ? "·"
                        : (printRawNonEmptyValues ? data[i, j].ToString() : "X"));
                }
                output.WriteLine();
            }
        }
    }
}