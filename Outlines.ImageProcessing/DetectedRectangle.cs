using System.Drawing;

namespace Outlines.ImageProcessing
{
    public class DetectedRectangle
    {
        public Rectangle Rect { get; private set; }
        public Line Left { get; private set; }
        public Line Top { get; private set; }
        public Line Right { get; private set; }
        public Line Bottom { get; private set; }

        public DetectedRectangle(Rectangle rect, Line left, Line top, Line right, Line bottom)
        {
            Rect = rect;
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
