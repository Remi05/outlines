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
        public bool IsDashedLine { get; private set; }

        public DistanceOutline(Point startPoint, Point endPoint, bool isDashedLine = false)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            IsDashedLine = isDashedLine;
            MidPoint = Point.Add(StartPoint, Point.Subtract(EndPoint, StartPoint) / 2);
            Distance = Point.Subtract(EndPoint, StartPoint).Length;
        }
    }
}
