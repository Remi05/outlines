using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;

namespace Redlines
{
    public class CustomElementProvider  : IElementProvider
    {
        private Condition FitlerCondition { get; set; }

        public CustomElementProvider()
        {
            
            FitlerCondition = new AndCondition(new NotCondition(new PropertyCondition(AutomationElement.NameProperty, "redlines")),
                                               new PropertyCondition(AutomationElement.IsOffscreenProperty, false));
        }

        public AutomationElement TryGetElementFromPoint(Point point)
        {
            var res = GetContainingElement(AutomationElement.RootElement, point);
            return res;
        }

        private AutomationElement GetContainingElement(AutomationElement rootElement, Point point)
        {
            if (rootElement == null)
            {
                return null;
            }

            try
            {
                Rect elementBounds = rootElement.Current.BoundingRectangle;

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
