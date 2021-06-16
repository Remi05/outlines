using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Outlines;

namespace OutlinesApp.Services
{
    public class InputMaskingService : IInputMaskingService
    {
        private FrameworkElement RootElement { get; set; }
        private ISet<FrameworkElement> IgnoreSet { get; set; } = new HashSet<FrameworkElement>();

        public InputMaskingService(FrameworkElement rootElement)
        {
            RootElement = rootElement;
        }

        public bool IsInInputMask(Point screenPoint)
        {
            if (RootElement == null)
            {
                return false;
            }

            Point localPoint = RootElement.PointFromScreen(screenPoint);
            var elementAtPoint = RootElement.InputHitTest(localPoint) as FrameworkElement;

            return elementAtPoint != null 
                && !IgnoreSet.Contains(elementAtPoint) 
                && !IgnoreSet.Any(ignoredElement => elementAtPoint.IsDescendantOf(ignoredElement));
        }

        public void Ignore(FrameworkElement element)
        {
            IgnoreSet.Add(element);
        }
    }
}
