using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Outlines.Core;

namespace Outlines.App.ViewModels
{
    public class UITreeItemViewModel : INotifyPropertyChanged
    {
        public UITreeNode UITreeNode { get; private set; }

        public string ElementName
        {
            get
            {
                string name = UITreeNode.ElementProperties.Name;
                return (string.IsNullOrWhiteSpace(name) ? "<unnamed>" : name) + $" - {UITreeNode.ElementProperties.ControlType}";
            }
        }

        public ObservableCollection<UITreeItemViewModel> ChildrenElements { get; private set; } = new ObservableCollection<UITreeItemViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public UITreeItemViewModel(UITreeNode uiTreeNode)
        {
            if (uiTreeNode == null)
            {
                throw new ArgumentNullException(nameof(uiTreeNode));
            }
            UITreeNode = uiTreeNode;
            UpdateChildrenElements();
        }

        private void UpdateChildrenElements()
        {
            foreach (var child in UITreeNode.Children)
            {
                ChildrenElements.Add(new UITreeItemViewModel(child));
            }
        }
    }
}
