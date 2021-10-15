using System;
using System.Drawing;
using Outlines.Core;

namespace Outlines.Inspection
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

        public Image TakeScreenshot(Rectangle bounds)
        {
            Image screenshot = new Bitmap(bounds.Width, bounds.Height);
            HideOverlay?.Invoke();
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(bounds.TopLeft(), Point.Empty, bounds.Size);
            }
            RestoreOverlay?.Invoke();
            return screenshot;
        }
    }
}
