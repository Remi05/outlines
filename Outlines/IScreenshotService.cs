using System.Drawing;

namespace Outlines
{
    public interface IScreenshotService
    {
        Bitmap TakeScreenshot(Rectangle rect);
    }
}