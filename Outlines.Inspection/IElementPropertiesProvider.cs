using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public interface IElementPropertiesProvider
    {
        ElementProperties GetElementProperties(IUIAutomationElement element);
    }
}