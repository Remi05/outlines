using System;
using System.Diagnostics;
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

        private readonly int CurrentProcessId = Process.GetCurrentProcess().Id;

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

            if (ShouldIgnoreElement(containingElement))
            {
                return null;
            }

            return PropertiesProvider.GetElementProperties(containingElement);
        }

        private bool ShouldIgnoreElement(IUIAutomationElement automationElement)
        {
            // We want to ignore any element of the current application.
            return (automationElement == null) && (automationElement.CurrentProcessId == CurrentProcessId);
        }
    }
}
