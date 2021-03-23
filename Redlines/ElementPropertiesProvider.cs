using System.Windows.Automation;

namespace Redlines
{
    public class ElementPropertiesProvider
    {
        // Defined at https://docs.microsoft.com/en-us/windows/win32/winauto/uiauto-automation-element-propids 
        const int UIA_FillColorPropertyId = 30160;
        const int UIA_OutlineColorPropertyId = 30161;
        const int UIA_OutlineThicknessPropertyId = 30164;

        public ElementProperties GetElementProperties(AutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            var elementProperties = new ElementProperties()
            {
                BoundingRect = element.Current.BoundingRectangle,
            };

            return elementProperties;
        }
    }
}
