using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using Outlines.Core;

namespace OutlinesApp.ViewModels
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
            UITreeService.RootNodeChanged += UpdateUiTreeViewElements;
            UpdateUiTreeViewElements();
        }

        public void OnElementSelectionChanged(UITreeItemViewModel uiTreeItemViewModel)
        {
            OutlinesService.SelectElementWithProperties(uiTreeItemViewModel?.UITreeNode?.ElementProperties);
        }

        private void UpdateUiTreeViewElements()
        {
            Dispatcher.Invoke(() =>
            {
                Elements.Clear();
                if (UITreeService.RootNode != null)
                {
                    var uiTreeItemViewModel = new UITreeItemViewModel(UITreeService.RootNode);
                    Elements.Add(uiTreeItemViewModel);
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Elements)));
            });
        }
    }
}
