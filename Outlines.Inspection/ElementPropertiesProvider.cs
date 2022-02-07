using System;
using System.Drawing;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class ElementPropertiesProvider : IElementPropertiesProvider
    {
        public ElementProperties GetElementProperties(IUIAutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            try
            {
                string name = element.CurrentName;
                string controlTypeName = element.CurrentLocalizedControlType;
                string automationId = element.CurrentAutomationId ?? "";
                string className = element.CurrentClassName ?? "";
                Rectangle boundingRect = element.CurrentBoundingRectangle.ToDrawingRectangle();

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

        private TextProperties GetTextProperties(IUIAutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            var textPattern = element.GetCurrentPattern(UIA_PatternIds.UIA_TextPatternId) as IUIAutomationTextPattern;
            if (textPattern == null)
            {
                return null;
            }

            var textProperties = new TextProperties()
            {
                FontName = textPattern.DocumentRange.GetAttributeValue(UIA_TextAttributeIds.UIA_FontNameAttributeId).ToString(),
                FontSize = textPattern.DocumentRange.GetAttributeValue(UIA_TextAttributeIds.UIA_FontSizeAttributeId).ToString(),
                FontWeight = textPattern.DocumentRange.GetAttributeValue(UIA_TextAttributeIds.UIA_FontWeightAttributeId).ToString(),
                ForegroundColor = textPattern.DocumentRange.GetAttributeValue(UIA_TextAttributeIds.UIA_ForegroundColorAttributeId).ToString(),
            };

            return textProperties;
        }
    }
}
