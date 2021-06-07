using Outlines;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;

namespace OutlinesApp.ViewModels
{
    public class ToolBarViewModel
    {
        private InspectorViewModel InspectorViewModel { get; set; }
        private IOutlinesService OutlinesService { get; set; }
        private IScreenshotService ScreenshotService { get; set; }
        
        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> TakeScreenshotCommand { get; private set; }
        public RelayCommand<object> ToggleOverlayCommand { get; private set; }
        public RelayCommand<object> TogglePropertiesPanelCommand { get; private set; }

        public ToolBarViewModel(InspectorViewModel inspectorViewModel, IOutlinesService outlinesService, IScreenshotService screenshotService)
        {
            InspectorViewModel = inspectorViewModel;
            OutlinesService = outlinesService;
            ScreenshotService = screenshotService;

            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown());
            TakeScreenshotCommand = new RelayCommand<object>(_ => TakeScreenshot());
            ToggleOverlayCommand = new RelayCommand<object>(_ => InspectorViewModel.ToggleOverlay());
            TogglePropertiesPanelCommand = new RelayCommand<object>(_ => InspectorViewModel.TogglePropertiesPanel());
        }

        private void TakeScreenshot()
        {
            if (OutlinesService.SelectedElementProperties != null)
            {
                Rect rect = OutlinesService.SelectedElementProperties.BoundingRect;
                Rectangle screenshotRect = new Rectangle((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
                Bitmap screenshot = ScreenshotService.TakeScreenshot(screenshotRect);
                screenshot.Save("screenshot.png", ImageFormat.Png);
            }
        }
    }
}
