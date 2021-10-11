using System.Drawing;
using System.Windows;

namespace Outlines
{
    public interface IScreenshotService
    {
        Image TakeScreenshot(ElementProperties elementProperties);
        Image TakeScreenshot(Rect bounds);
    }
}