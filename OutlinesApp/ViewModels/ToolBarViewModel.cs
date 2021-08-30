using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class ToolBarViewModel
    {
        private IOutlinesService OutlinesService { get; set; }
        private IScreenshotService ScreenshotService { get; set; }
        private ISnapshotService SnapshotService { get; set; }
        private IFolderConfig FolderConfig { get; set; }
        public InspectorViewModel InspectorViewModel { get; set; }

        public bool IsScreenshotButtonVisible => ScreenshotService != null;
        public bool IsSnapshotButtonVisible => SnapshotService != null;

        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> GetHelpCommand { get; private set; }
        public RelayCommand<object> GiveFeedbackCommand { get; private set; }
        public RelayCommand<object> ShowMoreInfoCommand { get; private set; }
        public RelayCommand<object> TakeScreenshotCommand { get; private set; }
        public RelayCommand<object> TakeSnapshotCommand { get; private set; }

        public ToolBarViewModel(IOutlinesService outlinesService, IScreenshotService screenshotService, ISnapshotService snapshotService, IFolderConfig folderConfig, InspectorViewModel inspectorViewModel)
        {
            if (outlinesService == null || folderConfig == null || inspectorViewModel == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService)
                                              : folderConfig == null ? nameof(folderConfig)
                                              : nameof(inspectorViewModel));
            }
            OutlinesService = outlinesService;
            ScreenshotService = screenshotService;
            SnapshotService = snapshotService;
            FolderConfig = folderConfig;
            InspectorViewModel = inspectorViewModel;

            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown(0));
            GetHelpCommand = new RelayCommand<object>(_ => GetHelp());
            GiveFeedbackCommand = new RelayCommand<object>(_ => GiveFeedback());
            ShowMoreInfoCommand = new RelayCommand<object>(_ => ShowMoreInfo());
            TakeScreenshotCommand = new RelayCommand<object>(_ => TakeScreenshot());
            TakeSnapshotCommand = new RelayCommand<object>(_ => TakeSnapshot());
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
            Image screenshot;
            if (OutlinesService.SelectedElementProperties != null)
            {
                screenshot = ScreenshotService.TakeScreenshot(OutlinesService.SelectedElementProperties);
            }
            else
            {
                var window = App.Current.MainWindow;
                //var windowBounds = new Rectangle((int)window.Left, (int)window.Top, (int)window.Width, (int)window.Height);
                var windowBounds = new Rectangle(0, 0, 1920, 1080);
                screenshot = ScreenshotService.TakeScreenshot(windowBounds);
            }

            string fileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
            string filePath = Path.Combine(FolderConfig.GetScreenshotsFolderPath(), fileName);
            screenshot.Save(filePath, ImageFormat.Png);
        }

        private void TakeSnapshot()
        {
            Snapshot snapshot;
            if (OutlinesService.SelectedElementProperties != null)
            {
                snapshot = SnapshotService.TakeSnapshot(OutlinesService.SelectedElementProperties);
            }
            else
            {
                var window = App.Current.MainWindow;
                var windowBounds = new Rectangle((int)window.Left, (int)window.Top, (int)window.Width, (int)window.Height);
                snapshot = SnapshotService.TakeSnapshot(windowBounds);
            }
            SnapshotService.SaveSnapshot(snapshot);
        }
    }
}
