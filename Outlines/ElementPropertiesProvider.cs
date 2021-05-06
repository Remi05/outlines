using System;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public class ElementPropertiesProvider
    {
        public ElementProperties GetElementProperties(AutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            try 
            { 
                string name = element.Current.Name;
                Rect boundingRect = element.Current.BoundingRectangle;
                return new ElementProperties(name, boundingRect);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
