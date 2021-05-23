using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace OutlinesApp.Services
{
    public class ScreenHelper : IScreenHelper
    {
        private Visual RootVisual { get; set; }

        public ScreenHelper(Visual rootVisual)
        {
            RootVisual = rootVisual;
        }

        public Rect GetMonitorRect(Point point)
        {
            var drawingPoint = new System.Drawing.Point((int)point.X, (int)point.Y);
            var drawingRect = Screen.FromPoint(drawingPoint).Bounds;
            return new Rect(new Point(drawingRect.Left, drawingRect.Top), new Size(drawingRect.Width, drawingRect.Height));
        }

        public Point PointFromScreen(Point screenPoint)
        {
            return RootVisual.PointFromScreen(screenPoint);
        }

        public Point PointToScreen(Point localPoint)
        {
            return RootVisual.PointToScreen(localPoint);
        }

        public Size SizeFromScreen(Size screenSize)
        {
            Matrix transformFromDevice = PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformFromDevice;
            Vector screenSizeVector = new Vector(screenSize.Width, screenSize.Height);
            Vector localSizeVector = transformFromDevice.Transform(screenSizeVector);
            return new Size(localSizeVector.X, localSizeVector.Y);
        }

        public Size SizeToScreen(Size localSize)
        {
            Matrix transformToDevice = PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformToDevice;
            Vector localSizeVector = new Vector(localSize.Width, localSize.Height);
            Vector screenSizeVector = transformToDevice.Transform(localSizeVector);
            return new Size(screenSizeVector.X, screenSizeVector.Y);
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
