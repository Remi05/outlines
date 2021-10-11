using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp.Views
{
    public partial class RectangleSelection : UserControl
    {
        private RectangleSelectionViewModel ViewModel { get; set; }

        public RectangleSelection()
        {
            InitializeComponent();
        }

        private Point GetRelativePosition(Point localPosition)
        {
            return (ActualWidth == 0 || ActualHeight == 0) ? new Point(0, 0) : new Point(localPosition.X / ActualWidth, localPosition.Y / ActualHeight);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel = ServiceContainer.Instance.GetService<RectangleSelectionViewModel>();
            Root.DataContext = ViewModel;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            ViewModel.OnMouseDown(GetRelativePosition(e.GetPosition(this)));
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            ViewModel.OnMouseUp(GetRelativePosition(e.GetPosition(this)));
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            ViewModel.OnMouseMove(GetRelativePosition(e.GetPosition(this)));
        }
    }
}
