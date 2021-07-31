using System.Windows;
using System.Windows.Forms;

namespace OutlinesApp.Services
{
    public class ScreenHelper : IScreenHelper
    {
        public Rect GetMonitorRect(Point point)
        {
            var drawingPoint = new System.Drawing.Point((int)point.X, (int)point.Y);
            var drawingRect = Screen.FromPoint(drawingPoint).Bounds;
            return new Rect(new Point(drawingRect.Left, drawingRect.Top), new Size(drawingRect.Width, drawingRect.Height));
        }
    }
}
