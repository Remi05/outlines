using System.Windows;
using System.Windows.Controls;
using Outlines.App.Services;
using Outlines.App.ViewModels;

namespace Outlines.App.Views
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
            var inputMaskingService = ServiceContainer.Instance.GetService<InputMaskingService>();
            inputMaskingService?.Ignore(this);
        }
    }
}
