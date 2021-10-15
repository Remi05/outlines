using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Outlines.App.Services;
using Outlines.App.ViewModels;

namespace Outlines.App.Views
{
    public partial class SnapshotInspectorPage : Page
    {
        private SnapshotInspectorViewModel ViewModel { get; set; }

        public SnapshotInspectorPage()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel = ServiceContainer.Instance.GetService<SnapshotInspectorViewModel>();
            Root.DataContext = ViewModel;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.OnMouseDown(e.GetPosition(sender as IInputElement).ToDrawingPoint());
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            ViewModel.OnMouseMove(e.GetPosition(sender as IInputElement).ToDrawingPoint());
        }

        private void OnMouseWheelScroll(object sender, MouseWheelEventArgs e)
        {
            ViewModel.OnMouseWheelScroll(e.Delta);
        }
    }
}
