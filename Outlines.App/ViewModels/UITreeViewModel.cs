﻿using System;
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
        private UITreeItemViewModel SelectedElementViewModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public UITreeViewModel(Dispatcher dispatcher, IOutlinesService outlinesService, IUITreeService uiTreeService)
        {
            if (dispatcher == null || outlinesService == null || uiTreeService == null)
            {
                throw new ArgumentNullException(dispatcher == null ? nameof(dispatcher) : outlinesService == null ? nameof(outlinesService) : nameof(uiTreeService));
            }
            Dispatcher = dispatcher;
            OutlinesService = outlinesService;
            OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
            UITreeService = uiTreeService;
            UITreeService.RootNodeChanged += UpdateUiTreeViewElements;
            UpdateUiTreeViewElements();
        }

        public void OnElementSelectionChanged(UITreeItemViewModel uiTreeItemViewModel)
        {
            OutlinesService.SelectElementWithProperties(uiTreeItemViewModel?.UITreeNode?.ElementProperties);
        }

        private void OnSelectedElementChanged()
        {
            if (SelectedElementViewModel != null)
            {
                SelectedElementViewModel.IsSelected = false;
            }

            var selectedElementProperties = OutlinesService.SelectedElementProperties;
            if (selectedElementProperties == null)
            {
                SelectedElementViewModel = null;
                return;
            }

            foreach (var elementViewModel in Elements)
            {
                var selectedElementViewModel = elementViewModel.FindViewModelFromElementProperties(selectedElementProperties);
                if (selectedElementViewModel != null)
                {
                    SelectedElementViewModel = selectedElementViewModel;
                    SelectedElementViewModel.IsSelected = true;
                    return;
                }
            }
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
