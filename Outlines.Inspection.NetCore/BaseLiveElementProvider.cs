using System;
using System.Drawing;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection.NetCore
{
    public class BaseLiveElementProvider : IElementProvider
    {
        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        protected IElementPropertiesProvider PropertiesProvider { get; set; }
        private Action HideOverlayWindow { get; set; }
        private Action ShowOverlayWindow { get; set; }

        public BaseLiveElementProvider(IElementPropertiesProvider propertiesProvider, Action hideOverlayWindow = null, Action showOverlayWindow = null)
        {
            PropertiesProvider = propertiesProvider;
            HideOverlayWindow = hideOverlayWindow;
            ShowOverlayWindow = showOverlayWindow;
        }

        public virtual ElementProperties TryGetElementFromPoint(Point point)
        {
            HideOverlayWindow?.Invoke();
            var containingElement = UIAutomation.ElementFromPoint(point.ToAutomationPoint());
            ShowOverlayWindow?.Invoke();
            return PropertiesProvider.GetElementProperties(containingElement);
        }

        public virtual ElementProperties TryGetElementFromHandle(IntPtr handle)
        {
            var element = UIAutomation.ElementFromHandle(handle);
            return PropertiesProvider.GetElementProperties(element);
        }
    }
}
