using System.Drawing;

namespace Outlines.Core
{
    public interface ICoordinateConverter
    {
        Point PointFromScreen(Point screenPoint);
        Point PointToScreen(Point localPoint);
        Rectangle RectFromScreen(Rectangle screenRect);
        Rectangle RectToScreen(Rectangle localRect);
        Size SizeFromScreen(Size screenSize);
        Size SizeToScreen(Size localSize);
    }
}