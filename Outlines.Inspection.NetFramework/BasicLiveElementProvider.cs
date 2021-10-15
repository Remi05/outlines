using System;
using System.Drawing;
using System.Windows.Automation;
using Outlines.Core;

namespace Outlines.Inspection.NetFramework
{
    public class BasicLiveElementProvider : IElementProvider
    {
        private IElementPropertiesProvider PropertiesProvider { get; set; }
        private Action HideOverlayWindow { get; set; }
        private Action ShowOverlayWindow { get; set; }

        public BasicLiveElementProvider(IElementPropertiesProvider propertiesProvider, Action hideOverlayWindow = null, Action showOverlayWindow = null)
        {
            PropertiesProvider = propertiesProvider;
            HideOverlayWindow = hideOverlayWindow;
            ShowOverlayWindow = showOverlayWindow;
        }

        public ElementProperties TryGetElementFromPoint(Point point)
        {
            HideOverlayWindow?.Invoke();
            var containingElement = AutomationElement.FromPoint(point.ToWindowsPoint());
            ShowOverlayWindow?.Invoke();
            return PropertiesProvider.GetElementProperties(containingElement);
        }
    }
}
