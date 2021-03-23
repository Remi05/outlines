
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
    }
}
