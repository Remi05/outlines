using System;
using System.Drawing;
using System.Windows;

namespace Outlines
{
    public class ScreenshotService : IScreenshotService
    {
        private Action HideOverlay { get; set; }
        private Action RestoreOverlay { get; set; }

        public ScreenshotService(Action hideOverlay = null, Action restoreOverlay = null)
        {
            HideOverlay = hideOverlay;
            RestoreOverlay = restoreOverlay;
        }

        public Image TakeScreenshot(ElementProperties elementProperties)
        {
            Rect rect = elementProperties.BoundingRect;
            Rectangle screenshotRect = new Rectangle((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
            return TakeScreenshot(screenshotRect);
        }

        public Image TakeScreenshot(Rectangle rect)
        {
            Image screenshot = new Bitmap(rect.Width, rect.Height);
            HideOverlay?.Invoke();
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(rect.Location, System.Drawing.Point.Empty, rect.Size);
            }
            RestoreOverlay?.Invoke();
            return screenshot;
        }
    }
}
