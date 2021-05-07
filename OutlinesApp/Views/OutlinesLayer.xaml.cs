using System.Windows.Controls;
using OutlinesApp.ViewModels;
using Outlines;

namespace OutlinesApp.Views
{
    public partial class OutlinesLayer : UserControl
    {
        public OutlinesLayer()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = ServiceContainer.Instance.GetService<InspectorViewModel>();
        }
    }
}
