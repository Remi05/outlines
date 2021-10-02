using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public class ScopedLiveElementProvider : LiveElementProvider
    {
        protected INativeWindowService NativeWindowService { get; set; }

        public ScopedLiveElementProvider(IElementPropertiesProvider propertiesProvider, INativeWindowService nativeWindowService)
            : base(propertiesProvider)
        {
            NativeWindowService = nativeWindowService;
            FilterCondition = new PropertyCondition(AutomationElement.IsOffscreenProperty, false);
        }

        public override ElementProperties TryGetElementFromPoint(Point point)
        {
            IntPtr windowAtPoint = NativeWindowService.GetWindowFromPoint(point);
            var windowElement = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NativeWindowHandleProperty, windowAtPoint.ToInt32()));
            var containingElement = GetContainingElement(windowElement, point);
            return PropertiesProvider.GetElementProperties(containingElement);
        }
    }
}
