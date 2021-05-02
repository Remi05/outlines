using System;
using System.Windows;
using System.Windows.Automation;

namespace Redlines
{
    public class ElementPropertiesProvider
    {
        public ElementProperties GetElementProperties(AutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            string name = element.Current.Name;

            Rect boundingRect;
            try
            {
                boundingRect = element.Current.BoundingRectangle;
            }
            catch (Exception)
            {
                boundingRect = Rect.Empty;
            }

            return new ElementProperties(name, boundingRect);
        }
    }
}
