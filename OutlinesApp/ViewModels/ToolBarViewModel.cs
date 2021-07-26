using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class ToolBarViewModel
    {
        public InspectorViewModel InspectorViewModel { get; set; }   
        private IOutlinesService OutlinesService { get; set; }
        private IScreenshotService ScreenshotService { get; set; }
        
        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> GetHelpCommand { get; private set; }
        public RelayCommand<object> GiveFeedbackCommand { get; private set; }
        public RelayCommand<object> ShowMoreInfoCommand { get; private set; }
        public RelayCommand<object> TakeScreenshotCommand { get; private set; }

        public ToolBarViewModel(InspectorViewModel inspectorViewModel, IOutlinesService outlinesService, IScreenshotService screenshotService)
        {
            if (inspectorViewModel == null)
            {
                throw new ArgumentNullException(nameof(inspectorViewModel));
            }
            InspectorViewModel = inspectorViewModel;
            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown(0));
            GetHelpCommand = new RelayCommand<object>(_ => GetHelp());
            GiveFeedbackCommand = new RelayCommand<object>(_ => GiveFeedback());
            ShowMoreInfoCommand = new RelayCommand<object>(_ => ShowMoreInfo());
            OutlinesService = outlinesService;
            ScreenshotService = screenshotService;

            TakeScreenshotCommand = new RelayCommand<object>(_ => TakeScreenshot());
        }

        private void GetHelp()
        {
            Process.Start("https://github.com/Remi05/outlines/wiki/User-Guide");
        }

        private void GiveFeedback()
        {
            Process.Start("https://github.com/Remi05/outlines/issues");
        }

        private void ShowMoreInfo()
        {
            Process.Start("https://github.com/Remi05/outlines");
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
