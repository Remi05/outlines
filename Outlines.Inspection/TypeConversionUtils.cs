using System.Drawing;

namespace Outlines.Inspection
{
    public static class TypeConversionUtils
    {
        public static UIAutomationClient.tagPOINT ToAutomationPoint(this Point p)
        {
            return new UIAutomationClient.tagPOINT() { x = p.X, y = p.Y };
        }

        public static Rectangle ToDrawingRectangle(this UIAutomationClient.tagRECT rect)
        {
            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;
            return new Rectangle(rect.left, rect.top, width, height);
        }
    }
}
