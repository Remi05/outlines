using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class AutomationPropertiesWatcher
    {
        private static readonly TimeSpan RefreshRate = TimeSpan.FromMilliseconds(300);

        private IOutlinesService OutlinesService { get; set; }
        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }

        private IUIAutomationElement WatchedSelectedElement { get; set; }
        private CancellationTokenSource WatchSelectedElementTaskCancellationTokenSource { get; set; }

        public AutomationPropertiesWatcher(IOutlinesService outlinesService, IElementPropertiesProvider elementPropertiesProvider)
        {
            ElementPropertiesProvider = elementPropertiesProvider;
            OutlinesService = outlinesService;
            OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
        }

        private void OnSelectedElementChanged()
        {
            var selectedElementProperties = OutlinesService.SelectedElementProperties as AutomationElementProperties;
            if (selectedElementProperties != null)
            {
                if (!AreSameElement(WatchedSelectedElement, selectedElementProperties.Element))
                {
                    // Stop watching the previous selected element.
                    WatchSelectedElementTaskCancellationTokenSource?.Cancel();

                    WatchedSelectedElement = selectedElementProperties.Element;
                    WatchSelectedElementTaskCancellationTokenSource = new CancellationTokenSource();
                    StartWatchingSelectedElementProperties(WatchedSelectedElement, WatchSelectedElementTaskCancellationTokenSource.Token);
                }
            }
        }

        private bool AreSameElement(IUIAutomationElement firstElement, IUIAutomationElement secondElement)
        {
            // We don't consider two null IUIAutomationElement to be the same element since neither are an actual element.
            if ((firstElement == null) || (secondElement == null))
            {
                return false;
            }

            try
            {
                var firstElementRuntimeId = firstElement.GetRuntimeId();
                var secondElementRuntimeId = secondElement.GetRuntimeId();
                return firstElementRuntimeId.SequenceEqual(secondElementRuntimeId);
            }
            catch
            {
                // GetRuntimeId() can sometimes throw, but it is not fatal, so we should simply consider the two elements
                // to be different (even if the elements are in fact the same, the main drawback is performance).
                return false;
            }
        }

        private void StartWatchingSelectedElementProperties(IUIAutomationElement elementToWatch, CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                Thread.Sleep(RefreshRate);
                while (!cancellationToken.IsCancellationRequested)
                {
                    ElementProperties elementProperties = ElementPropertiesProvider.GetElementProperties(elementToWatch);
                    OutlinesService.SelectElementWithProperties(elementProperties);
                    Thread.Sleep(RefreshRate);
                }
            });
        }
    }
}
