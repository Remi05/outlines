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

        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
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

        public UITreeItemViewModel FindViewModelFromElementProperties(ElementProperties elementProperties)
        {
            if (UITreeNode.ElementProperties == elementProperties)
            {
                return this;
            }
            foreach (var child in ChildrenElements)
            {
                var childItemViewModel = child.FindViewModelFromElementProperties(elementProperties);
                if (childItemViewModel != null)
                {
                    return childItemViewModel;
                }    
            }
            return null;
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
