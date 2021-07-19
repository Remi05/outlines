using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class UiTreeViewModel : INotifyPropertyChanged
    {
        private IOutlinesService OutlinesService { get; set; }
        private IUiTreeService UiTreeService { get; set; }
        public ObservableCollection<UiTreeItemViewModel> Elements { get; private set; } = new ObservableCollection<UiTreeItemViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public UiTreeViewModel(IOutlinesService outlinesService, IUiTreeService uiTreeService)
        {
            if (outlinesService == null || uiTreeService == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService) : nameof(uiTreeService));
            }
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
            Elements.Clear();
            if (UiTreeService.RootNode != null)
            {
                var uiTreeItemViewModel = new UiTreeItemViewModel(UiTreeService.RootNode);
                Elements.Add(uiTreeItemViewModel);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Elements)));
        }
    }
}
