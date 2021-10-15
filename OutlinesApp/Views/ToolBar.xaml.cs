using System.Windows.Controls;
using Outlines.App.Services;
using Outlines.App.ViewModels;

namespace Outlines.App.Views
{
    public partial class ToolBar : UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Root.DataContext = ServiceContainer.Instance.GetService<ToolBarViewModel>();
        }
    }
}
