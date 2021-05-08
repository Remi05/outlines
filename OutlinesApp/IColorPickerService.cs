using System.Windows;
using System.Windows.Media;

namespace OutlinesApp
{
    public interface IColorPickerService
    {
        Color GetColorAt(Point point);
    }
}