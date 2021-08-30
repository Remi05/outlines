using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace OutlinesApp.Services
{
    public class LiveCoordinateConverter : ICoordinateConverter
    {
        private Visual RootVisual { get; set; }

        public LiveCoordinateConverter(Visual rootVisual)
        {
            if (rootVisual == null)
            {
                throw new ArgumentNullException(nameof(rootVisual));
            }
            RootVisual = rootVisual;
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
            if (screenSize.IsEmpty)
            {
                return screenSize;
            }
            Matrix transformFromDevice = PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformFromDevice;
            Vector screenSizeVector = new Vector(screenSize.Width, screenSize.Height);
            Vector localSizeVector = transformFromDevice.Transform(screenSizeVector);
            return new Size(localSizeVector.X, localSizeVector.Y);
        }

        public Size SizeToScreen(Size localSize)
        {
            if (localSize.IsEmpty)
            {
                return localSize;
            }
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
