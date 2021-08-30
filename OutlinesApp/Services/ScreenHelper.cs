using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Outlines;

namespace OutlinesApp.Services
{
    public class ScreenHelper : IScreenHelper
    {
        private Visual RootVisual { get; set; }

        public ScreenHelper(Visual rootVisual)
        {
            RootVisual = rootVisual;
        }

        public Rect GetDisplayRect(Point point)
        {
            var drawingPoint = new System.Drawing.Point((int)point.X, (int)point.Y);
            var drawingRect = Screen.FromPoint(drawingPoint).Bounds;
            return new Rect(new Point(drawingRect.Left, drawingRect.Top), new Size(drawingRect.Width, drawingRect.Height));
        }

        public double GetDisplayScaleFactor()
        {
            return PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformToDevice.M11;
        }
    }
}
