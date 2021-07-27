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
        
        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> GetHelpCommand { get; private set; }
        public RelayCommand<object> GiveFeedbackCommand { get; private set; }
        public RelayCommand<object> ShowMoreInfoCommand { get; private set; }
        public RelayCommand<object> TakeScreenshotCommand { get; private set; }
        public RelayCommand<object> TakeSnapshotCommand { get; private set; }

        public ToolBarViewModel(IOutlinesService outlinesService, IScreenshotService screenshotService, ISnapshotService snapshotService, IFolderConfig folderConfig, InspectorViewModel inspectorViewModel)
        {
            if (outlinesService == null || screenshotService == null || snapshotService == null || folderConfig == null || inspectorViewModel == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService) 
                                              : screenshotService == null ? nameof(screenshotService) 
                                              : snapshotService == null ? nameof(snapshotService) 
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
            TakeScreenshotCommand = new RelayCommand<object>(_ => TakeScreenshot(), __ => OutlinesService.SelectedElementProperties != null);
            TakeSnapshotCommand = new RelayCommand<object>(_ => TakeSnapshot(), __ => OutlinesService.SelectedElementProperties != null);
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
                string fileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
                string filePath = Path.Combine(FolderConfig.GetScreenshotsFolderPath(), fileName);
                Image screenshot = ScreenshotService.TakeScreenshot(OutlinesService.SelectedElementProperties);
                screenshot.Save(filePath, ImageFormat.Png);
            }
        }
        private void TakeSnapshot()
        {
            if (OutlinesService.SelectedElementProperties != null)
            {
                Snapshot snapshot = SnapshotService.TakeSnapshot(OutlinesService.SelectedElementProperties);
                SnapshotService.SaveSnapshot(snapshot);
            }
        }
    }
}
