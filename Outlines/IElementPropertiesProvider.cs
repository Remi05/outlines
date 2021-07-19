using System.Windows.Automation;

namespace Outlines
{
    public interface IElementPropertiesProvider
    {
        ElementProperties GetElementProperties(AutomationElement element);
    }
}