using System.Windows.Automation;
using Outlines.Core;

namespace Outlines.Inspection.NetFramework
{
    public interface IElementPropertiesProvider
    {
        ElementProperties GetElementProperties(AutomationElement element);
    }
}