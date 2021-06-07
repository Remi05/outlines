using System.Drawing;

namespace Outlines
{
    public class ScreenshotService : IScreenshotService
    {
        public Bitmap TakeScreenshot(Rectangle rect)
        {
            Bitmap screenshot = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
            }
            return screenshot;
        }
    }
}
