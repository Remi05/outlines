using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Redlines
{
    public class DistanceOutline
    {
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public double Distance { get; private set; }

        public DistanceOutline(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Distance = Point.Subtract(EndPoint, StartPoint).Length;
        }
    }
}
