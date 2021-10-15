using System.Drawing;

namespace Outlines.Core
{
    public class CachedCoordinateConverter : ICoordinateConverter
    {
        private Snapshot Snapshot { get; set; }
        private Point RootPosition => Snapshot.UITree.ElementProperties.BoundingRect.TopLeft();
        private double ScaleFactor => Snapshot.ScaleFactor;

        public CachedCoordinateConverter(Snapshot snapshot)
        {
            Snapshot = snapshot;
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
            return screenSize == Size.Empty ? Size.Empty : new Size((int)(screenSize.Width / ScaleFactor), (int)(screenSize.Height / ScaleFactor));
        }

        public Size SizeToScreen(Size localSize)
        {
            return localSize == Size.Empty ? Size.Empty : new Size((int)(localSize.Width * ScaleFactor), (int)(localSize.Height * ScaleFactor));
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
