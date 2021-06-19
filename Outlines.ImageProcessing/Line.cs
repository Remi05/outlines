using System;
using System.Drawing;

namespace Outlines.ImageProcessing
{
    public class Line
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }

        public Line(Point start, Point end)
        {
            if (start.X > end.X)
            {
                throw new ArgumentException($"{nameof(start)}.X should always be smaller or equal to {nameof(end)}.X");
            }
            if (start.Y > end.Y)
            {
                throw new ArgumentException($"{nameof(start)}.Y should always be smaller or equal to {nameof(end)}.Y");
            }
            Start = start;
            End = end;
        }
    }
}
