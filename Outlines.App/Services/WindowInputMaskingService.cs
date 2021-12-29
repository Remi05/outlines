using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Outlines.Core;
using Outlines.Inspection;

namespace Outlines.App.Services
{
    public class WindowInputMaskingService : IInputMaskingService
    {
        private ICoordinateConverter CoordinateConverter { get; set; }
        private ISet<Window> WindowsToIgnore { get; set; } = new HashSet<Window>();

        public WindowInputMaskingService(ICoordinateConverter coordinateConverter)
        {
            CoordinateConverter = coordinateConverter;
        }

        public bool IsInInputMask(System.Drawing.Point screenPoint)
        {
            var localPoint = CoordinateConverter.PointFromScreen(screenPoint);
            return WindowsToIgnore.Any(window => WindowContainsPoint(window, localPoint));
        }

        private bool WindowContainsPoint(Window window, System.Drawing.Point point)
        {
            var windowRect = new System.Drawing.Rectangle((int)window.Left, (int)window.Top, 
                                                          (int)window.ActualWidth, (int)window.ActualHeight);
            return windowRect.Contains(point);
        }

        public void Ignore(Window window)
        {
            if (window != null)
            {
                WindowsToIgnore.Add(window);
            }
        }
    }
}
