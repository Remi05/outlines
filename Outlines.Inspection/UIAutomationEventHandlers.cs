using UIAutomationClient;

namespace Outlines.Inspection
{
    internal delegate void StructureChangedHandler(IUIAutomationElement sender);

    internal class UIAutomationStructureChangedEventHandler : IUIAutomationStructureChangedEventHandler
    {
        private StructureChangedHandler StructureChangedHandler { get; set; }

        public UIAutomationStructureChangedEventHandler(StructureChangedHandler handler)
        {
            StructureChangedHandler = handler;
        }

        public void HandleStructureChangedEvent(IUIAutomationElement sender, StructureChangeType changeType, int[] runtimeId)
        {
            StructureChangedHandler?.Invoke(sender);
        }
    }
}
