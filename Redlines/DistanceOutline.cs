using System.Windows;

namespace Redlines
{
    public class DistanceOutline
    {
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public Point MidPoint { get; private set; }
        public double Distance { get; private set; }
        public bool IsVertical => StartPoint.X == EndPoint.X;

        public DistanceOutline(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            MidPoint = Point.Add(StartPoint, Point.Subtract(EndPoint, StartPoint) / 2);
            Distance = Point.Subtract(EndPoint, StartPoint).Length;
        }
    }
}
