using System.Drawing;

namespace Outlines
{
    public interface IScreenshotService
    {
        Image TakeScreenshot(ElementProperties elementProperties);
        Image TakeScreenshot(Rectangle rect);
    }
}