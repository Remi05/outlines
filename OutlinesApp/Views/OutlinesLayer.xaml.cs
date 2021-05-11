using System.Windows;
using System.Windows.Controls;
using OutlinesApp.ViewModels;

namespace OutlinesApp.Views
{
    public partial class OutlinesLayer : UserControl
    {
        public OutlinesLayer()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceContainer.Instance.GetService<OutlinesViewModel>();
        }
    }
}
