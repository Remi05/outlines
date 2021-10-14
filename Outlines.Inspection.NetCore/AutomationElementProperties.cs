using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection.NetCore
{
    public class AutomationElementProperties : ElementProperties
    {
        public IUIAutomationElement Element { get; set; }
    }
}
