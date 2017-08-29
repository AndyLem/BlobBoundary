using System.IO;
using Blob.App.Interfaces;
using Blob.App.Models;
using log4net;

namespace Blob.App.Detectors
{
    class RecursiveBoundaryDetector : BaseDetector
    {
        private int[,] _visited;
        private const int NotVisited = -1;
        private const int VisitedEmpty = 0;
        private const int VisitedBoundary = 1;
        private const int VisitedInside = 2;

        private Point _topLeft;
        private Point _bottomRight;

        private static readonly ILog Log = LogManager.GetLogger(typeof(RecursiveBoundaryDetector));

        public override Boundary DetectBoundary(IDataProvider data)
        {
            ResetVisitedMap();
            _topLeft = new Point(data.N, data.N);
            _bottomRight = new Point(0, 0);
            int i, j;
            Point start = null;
            for (i = 0; i < data.N; i++)
            {
                for (j = 0; j < data.N; j++)
                {
                    var value = data.Get(i, j);
                    if (value)
                    {
                        start = new Point(j, i);
                        break;
                    }
                    Visit(i, j, 0);
                }
                if (start != null) break;
            }
            if (start == null) return null;

            Log.Debug($"Found starting point {start.X},{start.Y}");

            CheckPoint(data, start.X, start.Y);

            return new Boundary(_topLeft, _bottomRight);
        }

        private void ResetVisitedMap()
        {
            _visited = new[,]
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
        }

        private void Visit(int i, int j, int value)
        {
            _visited[i, j] = value;
        }

        private bool IsVisited(int i, int j)
        {
            return _visited[i, j] != NotVisited;
        }

        private void CheckPoint(IDataProvider data, int x, int y)
        {
            if (IsVisited(y, x)) return;
            var value = data.Get(y, x);
            Visit(y, x, value? VisitedBoundary : VisitedEmpty);

            if (!value) return;
            if (x < _topLeft.X) _topLeft.X = x;
            if (x > _bottomRight.X) _bottomRight.X = x;
            if (y < _topLeft.Y) _topLeft.Y = y;
            if (y > _bottomRight.Y) _bottomRight.Y = y;

            if (y != 0) TryCheckPoint(data, x, y - 1);
            if (x != 0) TryCheckPoint(data, x - 1, y);
            if (y != data.N - 1) TryCheckPoint(data, x, y + 1);
            if (x != data.N - 1) TryCheckPoint(data, x + 1, y);
        }

        private void TryCheckPoint(IDataProvider data, int x, int y)
        {
            if (GuessIfCellInside(data, x, y).GetValueOrDefault(false)) return;
            CheckPoint(data, x, y);
        }
        
        private bool? GuessIfCellInside(IDataProvider data, int x, int y)
        {
            if (_visited[y, x] == VisitedInside) return true;

            var leftOccupied = GetOccupiedGuess(x - 1, y, data.N);
            var rightOccupied = GetOccupiedGuess(x + 1, y, data.N);
            var topOccupied = GetOccupiedGuess(x, y - 1, data.N);
            var bottomOccupied = GetOccupiedGuess(x, y + 1, data.N);

            if (!leftOccupied.HasValue
             || !rightOccupied.HasValue
             || !topOccupied.HasValue
             || !bottomOccupied.HasValue) return null;

            var inside = leftOccupied.Value && rightOccupied.Value && topOccupied.Value && bottomOccupied.Value;
            if (inside) _visited[y, x] = VisitedInside;
            return inside;
        }

        private bool? GetOccupiedGuess(int x, int y, int n)
        {
            if (x < 0) return true;
            if (x >= n) return true;
            if (y < 0) return true;
            if (y >= n) return true;
            return Guess(y, x);
        }
        private bool? Guess(int i, int j)
        {
            if (IsVisited(i, j)) return _visited[i, j] != VisitedEmpty;
            return null;
        }

        public override void PrintAdditionalInfo(IDataPrinter printer, TextWriter output)
        {
            output.WriteLine("\nVisited map");
            printer.Print(output, _visited, -1, true);
        }
    }
}