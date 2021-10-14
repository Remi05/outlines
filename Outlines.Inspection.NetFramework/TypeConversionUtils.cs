using Outlines.Core;

namespace Outlines.Inspection.NetFramework
{
    public static class TypeConversionUtils
    {
        public static System.Drawing.Point ToDrawingPoint(this System.Windows.Point windowsPoint)
        {
            return new System.Drawing.Point((int)windowsPoint.X, (int)windowsPoint.Y);
        }

        public static System.Windows.Point ToWindowsPoint(this System.Drawing.Point drawingPoint)
        {
            return new System.Windows.Point(drawingPoint.X, drawingPoint.Y);
        }

        public static System.Drawing.Rectangle ToDrawingRectangle(this System.Windows.Rect windowsRect)
        {
            return new System.Drawing.Rectangle(windowsRect.TopLeft.ToDrawingPoint(), windowsRect.Size.ToDrawingSize());
        }

        public static System.Windows.Rect ToWindowsRect(this System.Drawing.Rectangle drawingRectangle)
        {
            return new System.Windows.Rect(drawingRectangle.TopLeft().ToWindowsPoint(), drawingRectangle.Size.ToWindowsSize());
        }

        public static System.Drawing.Size ToDrawingSize(this System.Windows.Size windowsSize)
        {
            return new System.Drawing.Size((int)windowsSize.Width, (int)windowsSize.Height);
        }

        public static System.Windows.Size ToWindowsSize(this System.Drawing.Size drawingSize)
        {
            return new System.Windows.Size(drawingSize.Width, drawingSize.Height);
        }
    }
}
