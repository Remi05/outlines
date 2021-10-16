using System;
using System.Drawing;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection.NetCore
{
    public class FilteredLiveElementProvider : BaseLiveElementProvider
    {
        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        private IUIAutomationCondition FilterCondition { get; set; }

        public FilteredLiveElementProvider(IElementPropertiesProvider propertiesProvider)
            : base(propertiesProvider)
        {
            FilterCondition = UIAutomation.CreateAndCondition(UIAutomation.CreateNotCondition(UIAutomation.CreateAndCondition(UIAutomation.CreatePropertyConditionEx(UIA_PropertyIds.UIA_NamePropertyId, "Outlines", PropertyConditionFlags.PropertyConditionFlags_IgnoreCase),
                                                                                              UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_WindowControlTypeId))),
                                                              UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_IsOffscreenPropertyId, false));
        }

        public override ElementProperties TryGetElementFromPoint(Point point)
        {
            var containingElement = GetContainingElement(UIAutomation.GetRootElement(), point);
            return PropertiesProvider.GetElementProperties(containingElement);
        }

        private IUIAutomationElement GetContainingElement(IUIAutomationElement rootElement, Point point)
        {
            if (rootElement == null)
            {
                return null;
            }

            try
            {
                Rectangle elementBounds = rootElement.CurrentBoundingRectangle.ToDrawingRectangle();

                if (!elementBounds.Contains(point))
                {
                    return null;
                }

                var children = rootElement.FindAll(TreeScope.TreeScope_Children, FilterCondition);
                for(int i = 0; i < children.Length; ++i)
                {
                    try
                    {
                        var containingElement = GetContainingElement(children.GetElement(i), point);
                        if (containingElement != null)
                        {
                            return containingElement;
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }

            return rootElement;
        }
    }
}
