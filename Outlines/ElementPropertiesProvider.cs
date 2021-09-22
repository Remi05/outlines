using System;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public class ElementPropertiesProvider : IElementPropertiesProvider
    {
        private bool IsCached { get; set; }

        public ElementPropertiesProvider(bool isCached = false)
        {
            IsCached = isCached;
        }

        public ElementProperties GetElementProperties(AutomationElement element)
        {
            try
            {
                if (element == null)
                {
                    return null;
                }

                var elementInfo = IsCached ? element.Cached : element.Current;

                ControlType controlType = elementInfo.ControlType;
                string controlTypeName = controlType == null ? "" : controlType.ProgrammaticName.Replace("ControlType.", "").Trim();
                string name = elementInfo.Name ?? "";
                string automationId = elementInfo.AutomationId ?? "";
                string className = elementInfo.ClassName ?? "";
                Rect boundingRect = elementInfo.BoundingRectangle;

                return new ElementProperties()
                {
                    ControlType = controlTypeName,
                    Name = name,
                    AutomationId = automationId,
                    ClassName = className,
                    BoundingRect = boundingRect,
                    TextProperties = GetTextProperties(element),
                    Element = element,
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
            bool wasPatternFound = IsCached ? element.TryGetCachedPattern(TextPattern.Pattern, out textPatternObject)
                                            : element.TryGetCurrentPattern(TextPattern.Pattern, out textPatternObject);

            if (!wasPatternFound)
            {
                return null;
            }

            var textPatternRange = (textPatternObject as TextPattern).DocumentRange;
            var textProperties = new TextProperties()
            {
                FontName = textPatternRange.GetAttributeValue(TextPattern.FontNameAttribute).ToString(),
                FontSize = textPatternRange.GetAttributeValue(TextPattern.FontSizeAttribute).ToString(),
                FontWeight = textPatternRange.GetAttributeValue(TextPattern.FontWeightAttribute).ToString(),
                ForegroundColor = textPatternRange.GetAttributeValue(TextPattern.ForegroundColorAttribute).ToString(),
            };

            return textProperties;
        }
    }
}
