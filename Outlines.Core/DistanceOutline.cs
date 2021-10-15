using System.Drawing;

namespace Outlines.Core
{
    public class DistanceOutline
    {
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public Point MidPoint { get; private set; }
        public double Distance { get; private set; }
        public bool IsVertical => StartPoint.X == EndPoint.X;
        public bool IsDistanceLine { get; private set; }
        public bool IsAlignmentLine { get; private set; }

        public DistanceOutline(Point startPoint, Point endPoint, bool isDistanceLine, bool isAlignmentLine)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            IsDistanceLine = isDistanceLine;
            IsAlignmentLine = isAlignmentLine;
            MidPoint = StartPoint.Add(EndPoint.Subtract(StartPoint).Divide(2));
            Distance = EndPoint.Subtract(StartPoint).Length();
        }
    }
}
