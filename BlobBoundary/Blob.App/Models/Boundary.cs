namespace Blob.App.Models
{
    public class Boundary
    {
        public Point TopLeft;
        public Point BottomRight;

        public int Left => TopLeft.X;
        public int Top => TopLeft.Y;
        public int Right => BottomRight.X;
        public int Bottom => BottomRight.Y;

        public Boundary(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}