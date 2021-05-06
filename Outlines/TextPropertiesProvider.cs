using System.Windows.Automation;

namespace Outlines
{
    public class TextPropertiesProvider
    {
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
