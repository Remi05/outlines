using System.Drawing;
using Outlines.Core;

namespace Outlines.App.Services
{
    // Coordinate converter to be used when the screen and window coordinates are expected to match 1:1 (eg. when no DPI scaling is performed).
    public class DpiIndependentCoordinateConverter : ICoordinateConverter
    {
        public Point PointFromScreen(Point screenPoint) => screenPoint;
        public Point PointToScreen(Point localPoint) => localPoint;
        public Size SizeFromScreen(Size screenSize) => screenSize;
        public Size SizeToScreen(Size localSize) => localSize;
        public Rectangle RectFromScreen(Rectangle screenRect) => screenRect;
        public Rectangle RectToScreen(Rectangle localRect) => localRect;
    }
}
