using System.Windows;
using System.Windows.Controls;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp.Views
{
    public partial class OutlinesOverlay : UserControl
    {
        public OutlinesOverlay()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Root.DataContext = ServiceContainer.Instance.GetService<OverlayViewModel>();
        }
    }
}
