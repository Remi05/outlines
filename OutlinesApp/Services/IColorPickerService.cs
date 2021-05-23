using System.Windows;
using System.Windows.Media;

namespace OutlinesApp.Services
{
    public interface IColorPickerService
    {
        Color GetColorAt(Point point);
    }
}