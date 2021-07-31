using System.Windows;

namespace Outlines
{
    public interface IScreenHelper
    {
        Rect GetDisplayRect(Point point);
        double GetDisplayScaleFactor();
    }
}