using System.IO;

namespace Blob.App.Interfaces
{
    public interface IDataProvider
    {
        /// <summary>
        /// Check if the cell is occupied
        /// </summary>
        /// <param name="i">Row</param>
        /// <param name="j">Column</param>
        /// <returns>True if the cell is occupied</returns>
        bool Get(int i, int j);

        int ReadsCount { get; }

        /// <summary>
        /// Size of a square matrix
        /// </summary>
        int N { get; }

        void Print(IDataPrinter printer, TextWriter output);
    }
}