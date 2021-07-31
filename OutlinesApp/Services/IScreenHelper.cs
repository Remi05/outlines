using System.Windows;

namespace OutlinesApp.Services
{
    public interface IScreenHelper
    {
        Rect GetMonitorRect(Point point);
    }
}