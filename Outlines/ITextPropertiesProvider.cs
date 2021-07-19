using System.Windows.Automation;

namespace Outlines
{
    public interface ITextPropertiesProvider
    {
        TextProperties GetTextProperties(AutomationElement element);
    }
}