using System;
using System.Drawing;

namespace Outlines.Core
{
    public static class PointUtils
    {
        public static Point Add(this Point p, Point otherPoint)
        {
            return Point.Add(p, new Size(otherPoint.X, otherPoint.Y));
        }

        public static Point Subtract(this Point p, Point otherPoint)
        {
            return Point.Subtract(p, new Size(otherPoint.X, otherPoint.Y));
        }

        public static Point Multiply(this Point p, double factor)
        {
            return new Point((int)Math.Ceiling(p.X * factor), (int)Math.Ceiling(p.Y * factor));
        }

        public static Point Divide(this Point p, double divider)
        {
            return new Point((int)Math.Ceiling(p.X / divider), (int)Math.Ceiling(p.Y / divider));
        }

        public static double Length(this Point p)
        {
            return Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2));
        }
    }
}
