using System;
using System.Drawing;
using System.Windows.Media;
using Outlines.Core;

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
            return RootVisual.PointFromScreen(screenPoint.ToWindowsPoint()).ToDrawingPoint();
        }

        public Point PointToScreen(Point localPoint)
        {
            return RootVisual.PointToScreen(localPoint.ToWindowsPoint()).ToDrawingPoint();
        }

        public Size SizeFromScreen(Size screenSize)
        {
            if (screenSize.IsEmpty)
            {
                return screenSize;
            }
            Matrix transformFromDevice = System.Windows.PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformFromDevice;
            System.Windows.Vector screenSizeVector = new System.Windows.Vector(screenSize.Width, screenSize.Height);
            System.Windows.Vector localSizeVector = transformFromDevice.Transform(screenSizeVector);
            return new Size((int)localSizeVector.X, (int)localSizeVector.Y);
        }

        public Size SizeToScreen(Size localSize)
        {
            if (localSize.IsEmpty)
            {
                return localSize;
            }
            Matrix transformToDevice = System.Windows.PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformToDevice;
            System.Windows.Vector localSizeVector = new System.Windows.Vector(localSize.Width, localSize.Height);
            System.Windows.Vector screenSizeVector = transformToDevice.Transform(localSizeVector);
            return new Size((int)screenSizeVector.X, (int)screenSizeVector.Y);
        }

        public Rectangle RectFromScreen(Rectangle screenRect)
        {
            Point localPoint = PointFromScreen(screenRect.TopLeft());
            Size localSize = SizeFromScreen(screenRect.Size);
            return new Rectangle(localPoint, localSize);
        }

        public Rectangle RectToScreen(Rectangle localRect)
        {
            Point screenPoint = PointToScreen(localRect.TopLeft());
            Size screenSize = SizeToScreen(localRect.Size);
            return new Rectangle(screenPoint, screenSize);
        }
    }
}
