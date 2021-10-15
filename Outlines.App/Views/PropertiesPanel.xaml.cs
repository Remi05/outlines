using System.Windows.Controls;
using Outlines.App.Services;
using Outlines.App.ViewModels;

namespace Outlines.App.Views
{
    public partial class PropertiesPanel : UserControl
    {
        public PropertiesPanel()
        {
            InitializeComponent();         
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Root.DataContext = ServiceContainer.Instance.GetService<PropertiesViewModel>();
        }
    }
}
