using System.Drawing;
using Redlines;

namespace RedlinesApp
{
    public static class Helpers
    {
        public static System.Windows.Point DrawingPointToWindowsPoint(System.Drawing.Point drawingPoint)
        {
            return new System.Windows.Point(drawingPoint.X, drawingPoint.Y);
        }

        public static System.Drawing.Point WindowsPointToDrawingPoint(System.Windows.Point windowsPoint)
        {
            return new System.Drawing.Point((int)windowsPoint.X, (int)windowsPoint.Y);
        }

        public static System.Drawing.Size WindowsSizeToDrawingSize(System.Windows.Size windowsSize)
        {
            return new System.Drawing.Size((int)windowsSize.Width, (int)windowsSize.Height);
        }

        public static System.Drawing.Rectangle WindowsRectToDrawingRect(System.Windows.Rect windowsRect)
        {
            return new System.Drawing.Rectangle(WindowsPointToDrawingPoint(windowsRect.Location), WindowsSizeToDrawingSize(windowsRect.Size));
        }

        public static Rectangle GetTextRectangleForDistanceOutline(DistanceOutline distanceOutline)
        {
            const int valOffset = 12;
            Size offset = distanceOutline.IsVertical ? new Size(valOffset, 0) : new Size(0, valOffset);
            Point rectPos = Point.Add(WindowsPointToDrawingPoint(distanceOutline.MidPoint), offset);
            Size rectSize = new Size(50, 22);
            return new Rectangle(rectPos, rectSize);
        }

        public static Rectangle GetTextRectangleForOutlineRect(System.Windows.Rect outlineRect)
        {
            Size rectSize = new Size(85, 22);
            Size offset = new Size(-rectSize.Width / 2, 12);
            System.Windows.Point bottomCenter = System.Windows.Point.Add(outlineRect.BottomLeft, System.Windows.Point.Subtract(outlineRect.BottomRight, outlineRect.BottomLeft) / 2);
            Point rectPos = Point.Add(WindowsPointToDrawingPoint(bottomCenter), offset);
            return new Rectangle(rectPos, rectSize);
        }
    }
}
