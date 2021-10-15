using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Outlines.Inspection;

namespace Outlines.App.Services
{
    public class InputMaskingService : IInputMaskingService
    {
        private FrameworkElement RootElement { get; set; }
        private ISet<FrameworkElement> IgnoreSet { get; set; } = new HashSet<FrameworkElement>();

        public InputMaskingService(FrameworkElement rootElement)
        {
            RootElement = rootElement;
        }

        public bool IsInInputMask(System.Drawing.Point screenPoint)
        {
            if (RootElement == null)
            {
                return false;
            }

            Point localPoint = RootElement.PointFromScreen(screenPoint.ToWindowsPoint());
            var elementAtPoint = RootElement.InputHitTest(localPoint) as FrameworkElement;

            return elementAtPoint != null 
                && !IgnoreSet.Contains(elementAtPoint) 
                && !IgnoreSet.Any(ignoredElement => elementAtPoint.IsDescendantOf(ignoredElement));
        }

        public void Ignore(FrameworkElement element)
        {
            if (element != null)
            {
                IgnoreSet.Add(element);
            }
        }
    }
}
