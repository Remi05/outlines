using System;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public class LiveElementPropertiesProvider : IElementPropertiesProvider
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
                string automationId = element.Current.AutomationId ?? "";
                string className = element.Current.ClassName ?? "";
                Rect boundingRect = element.Current.BoundingRectangle;

                TextProperties textProperties = GetTextProperties(element);

                return new ElementProperties() { 
                    Name = name, 
                    ControlType = controlTypeName, 
                    BoundingRect = boundingRect, 
                    Element = element, 
                    TextProperties = textProperties,
                    AutomationId = automationId,
                    ClassName = className,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private TextProperties GetTextProperties(AutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            object textPatternObject;
            if (!element.TryGetCurrentPattern(TextPattern.Pattern, out textPatternObject))
            {
                return null;
            }

            TextPattern textPattern = (TextPattern)textPatternObject;
            var textProperties = new TextProperties()
            {
                FontName = textPattern.DocumentRange.GetAttributeValue(TextPattern.FontNameAttribute).ToString(),
                FontSize = textPattern.DocumentRange.GetAttributeValue(TextPattern.FontSizeAttribute).ToString(),
                FontWeight = textPattern.DocumentRange.GetAttributeValue(TextPattern.FontWeightAttribute).ToString(),
                ForegroundColor = textPattern.DocumentRange.GetAttributeValue(TextPattern.ForegroundColorAttribute).ToString(),
            };

            return textProperties;
        }
    }
}
