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
            return TakeScreenshot(elementProperties.BoundingRect);
        }

        public Image TakeScreenshot(Rect bounds)
        {
            Image screenshot = new Bitmap((int)bounds.Width, (int)bounds.Height);
            HideOverlay?.Invoke();
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                var location = new System.Drawing.Point((int)bounds.X, (int)bounds.Y);
                var size = new System.Drawing.Size((int)bounds.Width, (int)bounds.Height);
                g.CopyFromScreen(location, System.Drawing.Point.Empty, size);
            }
            RestoreOverlay?.Invoke();
            return screenshot;
        }
    }
}
