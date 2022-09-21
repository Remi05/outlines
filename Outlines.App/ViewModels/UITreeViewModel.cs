using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using Outlines.Core;

namespace Outlines.App.ViewModels
{
    public class UITreeViewModel : INotifyPropertyChanged
    {
        private Dispatcher Dispatcher { get; set; }
        private IOutlinesService OutlinesService { get; set; }
        private IUITreeService UITreeService { get; set; }
        public ObservableCollection<UITreeItemViewModel> Elements { get; private set; } = new ObservableCollection<UITreeItemViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public UITreeViewModel(Dispatcher dispatcher, IOutlinesService outlinesService, IUITreeService uiTreeService)
        {
            if (dispatcher == null || outlinesService == null || uiTreeService == null)
            {
                throw new ArgumentNullException(dispatcher == null ? nameof(dispatcher) : outlinesService == null ? nameof(outlinesService) : nameof(uiTreeService));
            }
            Dispatcher = dispatcher;
            OutlinesService = outlinesService;
            UITreeService = uiTreeService;
            UpdateUITreeViewElements();
        }

        public void OnElementSelectionChanged(UITreeItemViewModel uiTreeItemViewModel)
        {
            OutlinesService.SelectElementWithProperties(uiTreeItemViewModel?.UITreeNode?.ElementProperties);
        }

        private void UpdateUITreeViewElements()
        {
            Dispatcher.Invoke(() =>
            {
                Elements.Clear();
                if (UITreeService.RootNode != null)
                {
                    var uiTreeItemViewModel = new UITreeItemViewModel(Dispatcher, UITreeService.RootNode, true);
                    Elements.Add(uiTreeItemViewModel);
                }
            });
        }
    }
}
