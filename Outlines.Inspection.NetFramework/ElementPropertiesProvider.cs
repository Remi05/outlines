using System;
using System.Drawing;
using System.Windows.Automation;
using Outlines.Core;

namespace Outlines.Inspection.NetFramework
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
                string automationId = element.Current.AutomationId ?? "";
                string className = element.Current.ClassName ?? "";
                Rectangle boundingRect = element.Current.BoundingRectangle.ToDrawingRectangle();

                TextProperties textProperties = GetTextProperties(element);

                return new AutomationElementProperties() { 
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

        public TextProperties GetTextProperties(AutomationElement element)
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
