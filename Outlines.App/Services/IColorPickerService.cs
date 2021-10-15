using System.Windows.Media;

namespace Outlines.App.Services
{
    public interface IColorPickerService
    {
        Color GetColorAt(System.Drawing.Point point);
    }
}