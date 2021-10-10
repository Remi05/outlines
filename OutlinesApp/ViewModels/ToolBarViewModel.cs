using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using Outlines;
using OutlinesApp.Services;

namespace OutlinesApp.ViewModels
{
    public class ToolBarViewModel : INotifyPropertyChanged
    {
        private IOutlinesService OutlinesService { get; set; }
        private IScreenshotService ScreenshotService { get; set; }
        private ISnapshotService SnapshotService { get; set; }
        private IFolderConfig FolderConfig { get; set; }
        private ICoordinateConverter CoordinateConverter { get; set; }
        public InspectorViewModel InspectorViewModel { get; set; }

        public bool IsElementSnapshotButtonEnabled => OutlinesService?.SelectedElementProperties != null;
        public bool IsElementSnapshotButtonVisible => SnapshotService != null;
        public bool IsFullscreenSnapshotButtonVisible => SnapshotService != null;
        public bool IsScreenshotButtonVisible => ScreenshotService != null;

        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> GetHelpCommand { get; private set; }
        public RelayCommand<object> GiveFeedbackCommand { get; private set; }
        public RelayCommand<object> ShowMoreInfoCommand { get; private set; }
        public RelayCommand<object> TakeElementSnapshotCommand { get; private set; }
        public RelayCommand<object> TakeFullscreenSnapshotCommand { get; private set; }
        public RelayCommand<object> TakeScreenshotCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ToolBarViewModel(IOutlinesService outlinesService, IScreenshotService screenshotService, ISnapshotService snapshotService, 
                                IFolderConfig folderConfig, ICoordinateConverter coordinateConverter, InspectorViewModel inspectorViewModel)
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
            CoordinateConverter = coordinateConverter;
            InspectorViewModel = inspectorViewModel;

            OutlinesService.SelectedElementChanged += () => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsElementSnapshotButtonEnabled)));

            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown(0));
            GetHelpCommand = new RelayCommand<object>(_ => GetHelp());
            GiveFeedbackCommand = new RelayCommand<object>(_ => GiveFeedback());
            ShowMoreInfoCommand = new RelayCommand<object>(_ => ShowMoreInfo());
            TakeElementSnapshotCommand = new RelayCommand<object>(_ => TakeElementSnapshot());
            TakeFullscreenSnapshotCommand = new RelayCommand<object>(_ => TakeFullscreenSnapshot());
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
        private void TakeElementSnapshot()
        {
            if (OutlinesService.SelectedElementProperties != null)
            {
                Snapshot snapshot = SnapshotService.TakeSnapshot(OutlinesService.SelectedElementProperties);
                SnapshotService.SaveSnapshot(snapshot);
            }
        }

        private void TakeFullscreenSnapshot()
        {
            var window = App.Current.MainWindow;
            var windowBounds = new Rect(window.Left, window.Top, window.Width, window.Height);
            var screenWindowBounds = CoordinateConverter.RectToScreen(windowBounds);
            Snapshot snapshot = SnapshotService.TakeSnapshot(screenWindowBounds);
            SnapshotService.SaveSnapshot(snapshot);
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
                var windowBounds = new Rect(window.Left, window.Top, window.Width, window.Height);
                var screenWindowBounds = CoordinateConverter.RectToScreen(windowBounds);
                screenshot = ScreenshotService.TakeScreenshot(screenWindowBounds);
            }

            string fileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
            string filePath = Path.Combine(FolderConfig.GetScreenshotsFolderPath(), fileName);
            screenshot.Save(filePath, ImageFormat.Png);
        }

    }
}
