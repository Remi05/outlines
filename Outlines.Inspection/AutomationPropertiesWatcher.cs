using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class AutomationPropertiesWatcher
    {
        private delegate void AutomationPropertyChangedHandler(IUIAutomationElement sender);
        private delegate void AutomationEventHandler(IUIAutomationElement sender, int eventId);

        private class AutomationPropertyChangedHandlerImpl : IUIAutomationPropertyChangedEventHandler
        {
            private AutomationPropertyChangedHandler HandlerFunc { get; set; }

            public AutomationPropertyChangedHandlerImpl(AutomationPropertyChangedHandler handlerFunc)
            {
                HandlerFunc = handlerFunc;
            }

            public void HandlePropertyChangedEvent(IUIAutomationElement sender, int propertyId, object newValue)
            {
                HandlerFunc(sender);
            }
        }

        private class AutomationEventHandlerImpl : IUIAutomationEventHandler
        {
            private AutomationEventHandler HandlerFunc { get; set; }

            public AutomationEventHandlerImpl(AutomationEventHandler handlerFunc)
            {
                HandlerFunc = handlerFunc;
            }

            public void HandleAutomationEvent(IUIAutomationElement sender, int eventId)
            {
                HandlerFunc(sender, eventId);
            }
        }

        private static readonly int[] PropertiesToWatch = new int[]
        {
            UIA_PropertyIds.UIA_AutomationIdPropertyId,
            UIA_PropertyIds.UIA_BoundingRectanglePropertyId,
            UIA_PropertyIds.UIA_ClassNamePropertyId,
            UIA_PropertyIds.UIA_ControlTypePropertyId,
            UIA_PropertyIds.UIA_NamePropertyId,
            UIA_PropertyIds.UIA_IsOffscreenPropertyId,
            UIA_PropertyIds.UIA_ClickablePointPropertyId,
            UIA_PropertyIds.UIA_CenterPointPropertyId,
        };

        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        private IOutlinesService OutlinesService { get; set; }
        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }
        private IUIAutomationElement WatchedElement { get; set; }
        private IUIAutomationPropertyChangedEventHandler WatchedElementPropertyChangedEventHandler { get; set; }
        private IUIAutomationEventHandler WatchedElementEventHandler { get; set; }

        public AutomationPropertiesWatcher(IOutlinesService outlinesService, IElementPropertiesProvider elementPropertiesProvider)
        {
            ElementPropertiesProvider = elementPropertiesProvider;
            OutlinesService = outlinesService;

            WatchedElementPropertyChangedEventHandler = new AutomationPropertyChangedHandlerImpl(OnWatchedElementPropertyChanged);
            WatchedElementEventHandler = new AutomationEventHandlerImpl(OnWatchedElementEvent);
            OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
        }

        private void OnSelectedElementChanged()
        {
            if (WatchedElement != null)
            {
                UIAutomation.RemovePropertyChangedEventHandler(WatchedElement, WatchedElementPropertyChangedEventHandler);
                UIAutomation.RemoveAutomationEventHandler(UIA_EventIds.UIA_LayoutInvalidatedEventId, WatchedElement, WatchedElementEventHandler);
            }

            var automationElementProperties = OutlinesService.SelectedElementProperties as AutomationElementProperties;
            if (automationElementProperties != null)
            {
                WatchedElement = automationElementProperties.Element;
                UIAutomation.AddPropertyChangedEventHandler(WatchedElement, TreeScope.TreeScope_Element, null, 
                                                            WatchedElementPropertyChangedEventHandler, PropertiesToWatch);

                UIAutomation.AddAutomationEventHandler(UIA_EventIds.UIA_LayoutInvalidatedEventId, WatchedElement, 
                                                       TreeScope.TreeScope_Parent, null, WatchedElementEventHandler);
            }
        }

        private void OnWatchedElementPropertyChanged(IUIAutomationElement sender)
        {
            ElementProperties elementProperties = ElementPropertiesProvider.GetElementProperties(sender);
            OutlinesService.SelectElementWithProperties(elementProperties);
        }

        private void OnWatchedElementEvent(IUIAutomationElement sender, int eventId)
        {
            ElementProperties elementProperties = ElementPropertiesProvider.GetElementProperties(WatchedElement);
            OutlinesService.SelectElementWithProperties(elementProperties);
        }
    }
}
