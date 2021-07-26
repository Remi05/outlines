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
        private IOutlinesService OutlinesService { get; set; }
        private IScreenshotService ScreenshotService { get; set; }
        public InspectorViewModel InspectorViewModel { get; set; }   
        
        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> GetHelpCommand { get; private set; }
        public RelayCommand<object> GiveFeedbackCommand { get; private set; }
        public RelayCommand<object> ShowMoreInfoCommand { get; private set; }
        public RelayCommand<object> TakeScreenshotCommand { get; private set; }

        public ToolBarViewModel(IOutlinesService outlinesService, IScreenshotService screenshotService, InspectorViewModel inspectorViewModel)
        {
            if (outlinesService == null || screenshotService == null || inspectorViewModel == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService) : screenshotService == null ? nameof(screenshotService) : nameof(inspectorViewModel));
            }
            OutlinesService = outlinesService;
            ScreenshotService = screenshotService;
            InspectorViewModel = inspectorViewModel;

            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown(0));
            GetHelpCommand = new RelayCommand<object>(_ => GetHelp());
            GiveFeedbackCommand = new RelayCommand<object>(_ => GiveFeedback());
            ShowMoreInfoCommand = new RelayCommand<object>(_ => ShowMoreInfo());
            TakeScreenshotCommand = new RelayCommand<object>(_ => TakeScreenshot(), __ => OutlinesService.SelectedElementProperties != null);
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
            var elementProperties = OutlinesService.SelectedElementProperties;
            if (elementProperties != null)
            {
                App.Current.MainWindow.Hide();

                Rect rect = elementProperties.BoundingRect;
                Rectangle screenshotRect = new Rectangle((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
                Bitmap screenshot = ScreenshotService.TakeScreenshot(screenshotRect);
                screenshot.Save($"Screenshot-{DateTime.Now.ToFileTime()}.png", ImageFormat.Png);

                App.Current.MainWindow.Show();
            }
        }
    }
}
