using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class AutomationElementProperties : ElementProperties
    {
        public IUIAutomationElement Element { get; set; }
    }
}
