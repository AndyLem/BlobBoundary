using System;
using System.IO;
using Blob.App.Interfaces;
using Blob.App.Models;
using log4net;

namespace Blob.App.Detectors
{
    class DiagonalBoundaryDetector : BaseDetector
    {
        private Point _topLeft;
        private Point _bottomRight;
        private static readonly ILog Log = LogManager.GetLogger(typeof(DiagonalBoundaryDetector));

        public override Boundary DetectBoundary(IDataProvider data)
        {
            _topLeft = new Point(data.N, data.N);
            _bottomRight = new Point(0, 0);

            var Nm1 = data.N - 1;

            for (var i = 0; i < data.N; i++)
            {
                var j = Nm1 - i;
                int k;

                var pReads = data.ReadsCount;
                bool? getDiagI = null;
                bool? getDiagJ = null;
                Log.Debug($"Checking diagonal at ({i}, {i}) and ({j}, {j}). data.ReadsCount made: {data.ReadsCount - pReads}");

                Log.Debug($"\n_topLeft.X: current value {_topLeft.X}, position {i}, searching from 0 to {Math.Min(i, _topLeft.X)} at row {i}");
                pReads = data.ReadsCount;
                for (k = 0; k < Math.Min(i, _topLeft.X); k++)
                {
                    if (data.Get(i, k))
                    {
                        Log.Debug($"_topLeft.X: found at {k}");
                        _topLeft.X = k;
                        break;
                    }
                }
                if (_topLeft.X > i)
                {
                    getDiagI = data.Get(i, i);
                    if (getDiagI.Value)
                    {
                        _topLeft.X = i;
                        Log.Debug($"_topLeft.X: found at diagonal {i}");
                    }
                }
                Log.Debug($"data.ReadsCount made: {data.ReadsCount - pReads}");

                Log.Debug(
                    $"\nTOP: current value {_topLeft.Y}, position {i}, searching from 0 to {Math.Min(i, _topLeft.Y)} at column {i}");
                pReads = data.ReadsCount;
                for (k = 0; k < Math.Min(i, _topLeft.Y); k++)
                {
                    if (data.Get(k, i))
                    {
                        Log.Debug($"_topLeft.Y: found at {k}");
                        _topLeft.Y = k;
                        break;
                    }
                }
                if (_topLeft.Y > i)
                {
                    if (!getDiagI.HasValue) getDiagI = data.Get(i, i);
                    if (getDiagI.Value)
                    {
                        _topLeft.Y = i;
                        Log.Debug($"_topLeft.Y: found at diagonal {i}");
                    }
                }
                Log.Debug($"data.ReadsCount made: {data.ReadsCount - pReads}");

                Log.Debug(
                    $"\nRIGHT: current value {_bottomRight.X}, position {j}, searching from {Nm1} to {Math.Max(j, _bottomRight.X)} at row {j}");
                pReads = data.ReadsCount;
                for (k = Nm1; k > Math.Max(j, _bottomRight.X); k--)
                {
                    if (data.Get(j, k))
                    {
                        Log.Debug($"_bottomRight.X: found at {k}");
                        _bottomRight.X = k;
                        break;
                    }
                }
                if (_bottomRight.X < j)
                {
                    getDiagJ = data.Get(j, j);
                    if (getDiagJ.Value)
                    {
                        _bottomRight.X = j;
                        Log.Debug($"_bottomRight.X: found at diagonal {j}");
                    }
                }
                Log.Debug($"data.ReadsCount made: {data.ReadsCount - pReads}");

                Log.Debug(
                    $"\nBOTTOM: current value {_bottomRight.Y}, position {j}, searching from {Nm1} to {Math.Max(j, _bottomRight.Y)} at column {j}");
                pReads = data.ReadsCount;
                for (k = Nm1; k > Math.Max(j, _bottomRight.Y); k--)
                {
                    if (data.Get(k, j))
                    {
                        Log.Debug($"_bottomRight.Y: found at {k}");
                        _bottomRight.Y = k;
                        break;
                    }
                }
                if (_bottomRight.Y < j)
                {
                    if (!getDiagJ.HasValue) getDiagJ = data.Get(j, j);
                    if (getDiagJ.Value)
                    {
                        _bottomRight.Y = j;
                        Log.Debug($"_bottomRight.Y: found at diagonal {j}");
                    }
                }
            
                Log.Debug($"data.ReadsCount made: {data.ReadsCount - pReads}");

                Log.Debug($"\nCurrent total data.ReadsCount: {data.ReadsCount}");

                Log.Debug("\n------");
            }

            return new Boundary(_topLeft, _bottomRight);
        }
    }
}