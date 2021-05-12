using System.Windows.Controls;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp.Views
{
    public partial class PropertiesPanel : UserControl
    {
        public PropertiesPanel()
        {
            InitializeComponent();         
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = ServiceContainer.Instance.GetService<PropertiesViewModel>();
        }
    }
}
