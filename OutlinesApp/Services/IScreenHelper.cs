using System.Windows;

namespace OutlinesApp.Services
{
    public interface IScreenHelper
    {
        Rect GetDisplayRect(Point point);
    }
}