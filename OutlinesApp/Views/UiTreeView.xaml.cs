using System.Windows.Controls;
using Outlines.App.Services;
using Outlines.App.ViewModels;

namespace Outlines.App.Views
{
    public partial class UITreeView : UserControl
    {
        public UITreeView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Root.DataContext = ServiceContainer.Instance.GetService<UITreeViewModel>();
        }

        private void OnSelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            var uiTreeItemViewModel = ServiceContainer.Instance.GetService<UITreeViewModel>();
            uiTreeItemViewModel.OnElementSelectionChanged(e.NewValue as UITreeItemViewModel);          
        }
    }
}
