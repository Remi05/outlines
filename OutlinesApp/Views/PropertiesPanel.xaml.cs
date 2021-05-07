using System.Windows.Controls;
using OutlinesApp.ViewModels;

namespace OutlinesApp.Views
{
    public partial class PropertiesPanel : UserControl
    {
        public PropertiesViewModel ViewModel { get; private set; }

        public PropertiesPanel()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
