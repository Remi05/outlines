using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Root.DataContext = ServiceContainer.Instance.GetService<PropertiesViewModel>();
        }

        private void ElementProperty_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                Clipboard.SetText(textBlock.Text);

                ToolTip toolTip = new();
                toolTip.Content = "Copied!";
                toolTip.IsOpen = true;

                // Hide the ToolTip after 1s.
                Task.Delay(1000).ContinueWith(_ => Dispatcher.Invoke(() => toolTip.IsOpen = false));
            }
        }
    }
}
