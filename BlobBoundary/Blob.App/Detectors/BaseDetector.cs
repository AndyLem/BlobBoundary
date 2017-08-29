using System.IO;
using Blob.App.Interfaces;
using Blob.App.Models;

namespace Blob.App.Detectors
{
    public abstract class BaseDetector : IBoundaryDetector
    {
        public abstract Boundary DetectBoundary(IDataProvider data);

        public virtual void PrintAdditionalInfo(IDataPrinter printer, TextWriter output)
        {
            // no default output
        }
    }
}