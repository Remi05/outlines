using System.Drawing;

namespace Outlines.Core
{
    public static class RectangleUtils
    {
        public static Point BottomLeft(this Rectangle rect)
        {
            return new Point(rect.Left, rect.Bottom);
        }

        public static Point BottomRight(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Bottom);
        }

        public static Point TopLeft(this Rectangle rect)
        {
            return new Point(rect.Left, rect.Top);
        }

        public static Point TopRight(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Top);
        }
    }
}
