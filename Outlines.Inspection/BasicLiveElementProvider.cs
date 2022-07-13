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

        private readonly int CurrentProcessId = Process.GetCurrentProcess().Id;

        public BasicLiveElementProvider(IElementPropertiesProvider propertiesProvider)
        {
            PropertiesProvider = propertiesProvider;
        }

        public ElementProperties TryGetElementFromPoint(Point point)
        {
            var containingElement = UIAutomation.ElementFromPoint(point.ToAutomationPoint());

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
