using System;
using System.Drawing;
using System.Windows.Automation;
using Outlines.Core;

namespace Outlines.Inspection.NetFramework
{
    public class FilteredLiveElementProvider : IElementProvider
    {
        private IElementPropertiesProvider PropertiesProvider { get; set; }
        private Condition FitlerCondition { get; set; }

        public FilteredLiveElementProvider(IElementPropertiesProvider propertiesProvider)
        {
            PropertiesProvider = propertiesProvider;
            FitlerCondition = new AndCondition(new NotCondition(new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Outlines", PropertyConditionFlags.IgnoreCase), 
                                                                                 new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window))),
                                               new PropertyCondition(AutomationElement.IsOffscreenProperty, false));
        }

        public ElementProperties TryGetElementFromPoint(Point point)
        {
            var containingElement = GetContainingElement(AutomationElement.RootElement, point);
            return PropertiesProvider.GetElementProperties(containingElement);
        }

        private AutomationElement GetContainingElement(AutomationElement rootElement, Point point)
        {
            if (rootElement == null)
            {
                return null;
            }

            try
            {
                Rectangle elementBounds = rootElement.Current.BoundingRectangle.ToDrawingRectangle();

                if (!elementBounds.Contains(point))
                {
                    return null;
                }

                var children = rootElement.FindAll(TreeScope.Children, FitlerCondition);
                foreach (AutomationElement child in children)
                {
                    try
                    {
                        var containingElement = GetContainingElement(child, point);
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
