using System.Windows.Controls;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp.Views
{
    public partial class UiTreeView : UserControl
    {
        public UiTreeView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Root.DataContext = ServiceContainer.Instance.GetService<UiTreeViewModel>();
        }

        private void OnSelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            var uiTreeItemViewModel = ServiceContainer.Instance.GetService<UiTreeViewModel>();
            uiTreeItemViewModel.OnElementSelectionChanged(e.NewValue as UiTreeItemViewModel);          
        }
    }
}
