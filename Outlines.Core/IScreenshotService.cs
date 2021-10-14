using System.Drawing;

namespace Outlines.Core
{
    public interface IScreenshotService
    {
        Image TakeScreenshot(ElementProperties elementProperties);
        Image TakeScreenshot(Rectangle bounds);
    }
}