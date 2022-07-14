using System;
using System.Drawing;

namespace Outlines.Core
{
    public class CachedCoordinateConverter : ICoordinateConverter
    {
        private double ScaleFactor { get; set; }
        private Point Origin { get; set; }

        public CachedCoordinateConverter(double scaleFactor, Point origin)
        {
            ScaleFactor = scaleFactor;
            Origin = origin;
        }

        public CachedCoordinateConverter(Snapshot snapshot)
        {
            ScaleFactor = snapshot.ScaleFactor;
            Origin = snapshot.UITree.ElementProperties.BoundingRect.TopLeft();
        }

        public Point PointFromScreen(Point screenPoint)
        {
            return screenPoint.Subtract(Origin).Divide(ScaleFactor);
        }

        public Point PointToScreen(Point localPoint)
        {
            return localPoint.Multiply(ScaleFactor).Add(Origin);
        }

        public Size SizeFromScreen(Size screenSize)
        {
            return screenSize == Size.Empty ? Size.Empty : new Size((int)Math.Ceiling(screenSize.Width / ScaleFactor), (int)Math.Ceiling(screenSize.Height / ScaleFactor));
        }

        public Size SizeToScreen(Size localSize)
        {
            return localSize == Size.Empty ? Size.Empty : new Size((int)Math.Ceiling(localSize.Width * ScaleFactor), (int)Math.Ceiling(localSize.Height * ScaleFactor));
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
