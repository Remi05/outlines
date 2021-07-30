using System.Windows;

namespace Outlines
{
    public static class PointUtils
    {
        public static Point Add(this Point p, Point otherPoint)
        {
            return Point.Add(p, new Vector(otherPoint.X, otherPoint.Y));
        }

        public static Point Subtract(this Point p, Point otherPoint)
        {
            return Point.Subtract(p, new Vector(otherPoint.X, otherPoint.Y));
        }

        public static Point Multiply(this Point p, double factor)
        {
            return new Point(p.X * factor, p.Y * factor);
        }

        public static Point Divide(this Point p, double divider)
        {
            return new Point(p.X / divider, p.Y / divider);
        }
    }
}
