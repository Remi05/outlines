using System.Windows;

namespace OutlinesApp
{
    public interface IScreenHelper
    {
        Point PointFromScreen(Point screenPoint);
        Point PointToScreen(Point localPoint);
        Rect RectFromScreen(Rect screenRect);
        Rect RectToScreen(Rect localRect);
        Size SizeFromScreen(Size screenSize);
        Size SizeToScreen(Size localSize);
    }
}