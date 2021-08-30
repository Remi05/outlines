using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class UiTreeViewModel : INotifyPropertyChanged
    {
        private Dispatcher Dispatcher { get; set; }
        private IOutlinesService OutlinesService { get; set; }
        private IUiTreeService UiTreeService { get; set; }
        public ObservableCollection<UiTreeItemViewModel> Elements { get; private set; } = new ObservableCollection<UiTreeItemViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public UiTreeViewModel(Dispatcher dispatcher, IOutlinesService outlinesService, IUiTreeService uiTreeService)
        {
            if (dispatcher == null || outlinesService == null || uiTreeService == null)
            {
                throw new ArgumentNullException(dispatcher == null ? nameof(dispatcher) : outlinesService == null ? nameof(outlinesService) : nameof(uiTreeService));
            }
            Dispatcher = dispatcher;
            OutlinesService = outlinesService;
            UiTreeService = uiTreeService;
            UiTreeService.RootNodeChanged += UpdateUiTreeViewElements;
            UpdateUiTreeViewElements();
        }

        public void OnElementSelectionChanged(UiTreeItemViewModel uiTreeItemViewModel)
        {
            OutlinesService.SelectElementWithProperties(uiTreeItemViewModel?.UiTreeNode?.ElementProperties);
        }

        private void UpdateUiTreeViewElements()
        {
            Dispatcher.Invoke(() =>
            {
                Elements.Clear();
                if (UiTreeService.RootNode != null)
                {
                    var uiTreeItemViewModel = new UiTreeItemViewModel(UiTreeService.RootNode);
                    Elements.Add(uiTreeItemViewModel);
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Elements)));
            });
        }
    }
}
