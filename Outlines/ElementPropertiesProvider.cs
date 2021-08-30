using System;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public class ElementPropertiesProvider : IElementPropertiesProvider
    {
        private ITextPropertiesProvider TextPropertiesProvider { get; set; }

        public ElementPropertiesProvider(ITextPropertiesProvider textPropertiesProvider)
        {
            TextPropertiesProvider = textPropertiesProvider;
        }

        public ElementProperties GetElementProperties(AutomationElement element)
        {
            if (element?.Current == null)
            {
                return null;
            }

            try
            {
                string name = element.Current.Name;
                ControlType controlType = element.Current.ControlType;
                string controlTypeName = controlType == null ? "" : controlType.ProgrammaticName.Replace("ControlType.", "").Trim();
                Rect boundingRect = element.Current.BoundingRectangle;

                TextProperties textProperties = TextPropertiesProvider.GetTextProperties(element);

                return new ElementProperties() { 
                    Name = name, 
                    ControlType = controlTypeName, 
                    BoundingRect = boundingRect, 
                    Element = element, 
                    TextProperties = textProperties,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
