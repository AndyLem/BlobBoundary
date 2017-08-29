using System.IO;

namespace Blob.App.Interfaces
{
    public interface IDataPrinter
    {
        void Print(TextWriter output, int[,] data, int defaultEmptyValue = 0, bool printRawNonEmptyValues = false);
    }
}