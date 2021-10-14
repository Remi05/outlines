using System.Windows.Media;

namespace OutlinesApp.Services
{
    public interface IColorPickerService
    {
        Color GetColorAt(System.Drawing.Point point);
    }
}