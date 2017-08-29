using System.IO;
using Blob.App.Models;

namespace Blob.App.Interfaces
{
    public interface IBoundaryDetector
    {
        Boundary DetectBoundary(IDataProvider data);
        void PrintAdditionalInfo(IDataPrinter printer, TextWriter output);
    }
}