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
            Rect boundingRect = element.Current.BoundingRectangle;

            return new ElementProperties(name, boundingRect);
        }
    }
}
