using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class UiTreeItemViewModel : INotifyPropertyChanged
    {
        public UiTreeNode UiTreeNode { get; private set; }

        public string ElementName
        {
            get
            {
                string name = UiTreeNode.ElementProperties.Name;
                return (string.IsNullOrWhiteSpace(name) ? "<unnamed>" : name) + $" - {UiTreeNode.ElementProperties.ControlType}";
            }
        }

        public ObservableCollection<UiTreeItemViewModel> ChildrenElements { get; private set; } = new ObservableCollection<UiTreeItemViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public UiTreeItemViewModel(UiTreeNode uiTreeNode)
        {
            if (uiTreeNode == null)
            {
                throw new ArgumentNullException(nameof(uiTreeNode));
            }
            UiTreeNode = uiTreeNode;
            UpdateChildrenElements();
        }

        private void UpdateChildrenElements()
        {
            foreach (var child in UiTreeNode.Children)
            {
                ChildrenElements.Add(new UiTreeItemViewModel(child));
            }
        }
    }
}
