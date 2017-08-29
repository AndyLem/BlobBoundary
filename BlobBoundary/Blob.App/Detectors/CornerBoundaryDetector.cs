using System.IO;
using Blob.App.Interfaces;
using Blob.App.Models;
using log4net;

namespace Blob.App.Detectors
{
    class CornerBoundaryDetector : BaseDetector
    {
        private Point _topLeft;
        private Point _bottomRight;
        private static readonly ILog Log = LogManager.GetLogger(typeof(CornerBoundaryDetector));

        public override Boundary DetectBoundary(IDataProvider data)
        {
            _topLeft = new Point(data.N, data.N);
            _bottomRight = new Point(0, 0);

            for (var i = 0; i < data.N; i++)
            {
                var j = data.N - 1 - i;
                int k;


                if (_topLeft.X > i)
                {
                    for (k = 0; k < data.N; k++)
                    {
                        if (data.Get(k, i))
                        {
                            _topLeft.X = i;
                            break;
                        }
                    }
                }
                if (_topLeft.Y > i)
                {
                    for (k = 0; k < data.N; k++)
                    {
                        if (data.Get(i, k))
                        {
                            _topLeft.Y = i;
                            break;
                        }
                    }
                }

                if (_bottomRight.X < j)
                {
                    for (k = 0; k < data.N; k++)
                    {
                        if (data.Get(k, j))
                        {
                            _bottomRight.X = j;
                            break;
                        }
                    }
                }

                if (_bottomRight.Y < j)
                {
                    for (k = 0; k < data.N; k++)
                    {
                        if (data.Get(j, k))
                        {
                            _bottomRight.Y = j;
                            break;
                        }
                    }
                }

                Log.Debug($"\nCurrent total reads: {data.ReadsCount}");

                Log.Debug("\n------");
            }
            return new Boundary(_topLeft, _bottomRight);

        }
   }
}