using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection.NetCore
{
    public interface IElementPropertiesProvider
    {
        ElementProperties GetElementProperties(IUIAutomationElement element);
    }
}