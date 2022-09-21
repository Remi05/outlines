using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using Outlines.Core;

namespace Outlines.App.ViewModels
{
    public class UITreeItemViewModel : INotifyPropertyChanged
    {
        private Dispatcher Dispatcher { get; set; }
        public IUITreeNode UITreeNode { get; private set; }

        public string ElementName
        {
            get
            {
                if (UITreeNode == null)
                {
                    return "Loading...";
                }
                string name = UITreeNode.ElementProperties.Name;
                return (string.IsNullOrWhiteSpace(name) ? "<unnamed>" : name) + $" - {UITreeNode.ElementProperties.ControlType}";
            }
        }

        private bool shouldShowChildren = false;
        public bool ShouldShowChildren
        {
            get => shouldShowChildren;
            set
            {
                if (value != shouldShowChildren)
                {
                    shouldShowChildren = value;
                    if (shouldShowChildren && !WereChildrenLoaded)
                    {
                        UpdateChildrenElements();
                    }
                }
            }
        }

        private bool WereChildrenLoaded { get; set; } = false;
        public ObservableCollection<UITreeItemViewModel> ChildrenElements { get; private set; } = new ObservableCollection<UITreeItemViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public UITreeItemViewModel(Dispatcher dispatcher, IUITreeNode uiTreeNode, bool showChildren = false)
        {
            Dispatcher = dispatcher;
            if (uiTreeNode != null)
            {
                UITreeNode = uiTreeNode;
                UITreeNode.ChildrenChanged += UpdateChildrenElements;
                if (UITreeNode.HasChildren)
                {
                    // We ned to add a placeholder child so that users can expand the node
                    // (at which point we'll lazy-load the children and remove the placeholder).
                    ChildrenElements.Add(new UITreeItemViewModel(Dispatcher, null, false));
                }
            }
            ShouldShowChildren = showChildren;
        }

        private async void UpdateChildrenElements()
        {
            WereChildrenLoaded = true;
            var childrenElements = UITreeNode.GetAndMonitorChildren();

            Dispatcher.Invoke(() =>
            {
                ChildrenElements.Clear();
                foreach (var child in childrenElements)
                {
                    ChildrenElements.Add(new UITreeItemViewModel(Dispatcher, child));
                }
                ShouldShowChildren = true;
            });
        }
    }
}
