using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Outlines;

namespace OutlinesApp.Services
{
    public class CachedCoordinateConverter : IScreenHelper
    {
        public UiTreeNode UiTree { get; set; }
        private Point RootPosition => UiTree.ElementProperties.BoundingRect.TopLeft;
        private double ScaleFactor { get; set; } = 1.5;

        public Rect GetMonitorRect(Point point)
        {
            var drawingPoint = new System.Drawing.Point((int)point.X, (int)point.Y);
            var drawingRect = Screen.FromPoint(drawingPoint).Bounds;
            return new Rect(new Point(drawingRect.Left, drawingRect.Top), new Size(drawingRect.Width, drawingRect.Height));
        }

        public Point PointFromScreen(Point screenPoint)
        {
            return screenPoint.Subtract(RootPosition).Divide(ScaleFactor);
        }

        public Point PointToScreen(Point localPoint)
        {
            return localPoint.Multiply(ScaleFactor).Add(RootPosition);
        }

        public Size SizeFromScreen(Size screenSize)
        {
            return new Size(screenSize.Width / ScaleFactor, screenSize.Height / ScaleFactor);
        }

        public Size SizeToScreen(Size localSize)
        {
            return new Size(localSize.Width * ScaleFactor, localSize.Height * ScaleFactor);
        }

        public Rect RectFromScreen(Rect screenRect)
        {
            Point localPoint = PointFromScreen(screenRect.TopLeft);
            Size localSize = SizeFromScreen(screenRect.Size);
            return new Rect(localPoint, localSize);
        }

        public Rect RectToScreen(Rect localRect)
        {
            Point screenPoint = PointToScreen(localRect.TopLeft);
            Size screenSize = SizeToScreen(localRect.Size);
            return new Rect(screenPoint, screenSize);
        }
    }
}
