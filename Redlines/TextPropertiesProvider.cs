using System.Windows.Automation;

namespace Redlines
{
    public class TextPropertiesProvider
    {
        public TextProperties GetTextProperties(AutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            var textPattern = element.GetCurrentPattern(TextPattern.Pattern) as TextPattern;

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
