using System;
using System.Drawing;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class BasicLiveElementProvider : IElementProvider
    {
        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
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
            var containingElement = UIAutomation.ElementFromPoint(point.ToAutomationPoint());
            ShowOverlayWindow?.Invoke();
            return PropertiesProvider.GetElementProperties(containingElement);
        }
    }
}
