using System.Drawing;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection.NetCore
{
    public class LiveElementProvider2 : IElementProvider
    {
        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        private IElementPropertiesProvider PropertiesProvider { get; set; }
        private IUIAutomationCondition FilterCondition { get; set; }

        public LiveElementProvider2(IElementPropertiesProvider propertiesProvider)
        {
            PropertiesProvider = propertiesProvider;
            FilterCondition = UIAutomation.CreateAndCondition(UIAutomation.CreateNotCondition(UIAutomation.CreateAndCondition(UIAutomation.CreatePropertyConditionEx(UIA_PropertyIds.UIA_NamePropertyId, "Outlines", PropertyConditionFlags.PropertyConditionFlags_IgnoreCase),
                                                                                              UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_WindowControlTypeId))),
                                                              UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_IsOffscreenPropertyId, false));
        }

        public ElementProperties TryGetElementFromPoint(Point point)
        {
            var containingElement = UIAutomation.ElementFromPoint(point.ToAutomationPoint());
            return PropertiesProvider.GetElementProperties(containingElement);
        }
    }
}
