using System;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public class ElementPropertiesProvider : IElementPropertiesProvider
    {
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
                return new ElementProperties() { Name = name, ControlType = controlTypeName, BoundingRect = boundingRect, Element = element };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
