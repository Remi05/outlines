using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;

namespace Outlines
{
    public class SnapshotService : ISnapshotService
    {
        private IScreenshotService ScreenshotService { get; set; }
        private IUiTreeService UiTreeService { get; set; }
        private IScreenHelper ScreenHelper { get; set; }
        private IFolderConfig FolderConfig { get; set; }
        private bool ShouldSaveAsSingleFile { get; set; } = true;

        public SnapshotService(IScreenshotService screenshotService, IUiTreeService uiTreeService, IScreenHelper screenHelper, IFolderConfig folderConfig)
        {
            if (screenshotService == null || uiTreeService == null || screenHelper ==null || folderConfig == null)
            {
                throw new ArgumentNullException(screenshotService == null ? nameof(screenshotService)
                                              : uiTreeService == null ? nameof(uiTreeService)
                                              : screenHelper == null ? nameof(screenHelper)
                                              : nameof(folderConfig));
            }
            ScreenshotService = screenshotService;
            UiTreeService = uiTreeService;
            ScreenHelper = screenHelper;
            FolderConfig = folderConfig;
        }

        public Snapshot TakeSnapshot(Rectangle bounds)
        {
            UiTreeNode subtree = UiTreeService.GetSubTreeInBounds(bounds);
            Image screenshot = ScreenshotService.TakeScreenshot(bounds);
            double scaleFactor = ScreenHelper.GetDisplayScaleFactor();
            return new Snapshot() { UiTree = subtree, Screenshot = screenshot, ScaleFactor = scaleFactor };
        }

        public Snapshot TakeSnapshot(ElementProperties elementProperties)
        {
            UiTreeNode subtree = UiTreeService.GetSubTree(elementProperties);
            Image screenshot = ScreenshotService.TakeScreenshot(elementProperties);
            double scaleFactor = ScreenHelper.GetDisplayScaleFactor();
            return new Snapshot() { UiTree = subtree, Screenshot = screenshot, ScaleFactor = scaleFactor };
        }

        public void SaveSnapshot(Snapshot snapshot)
        {
            if (ShouldSaveAsSingleFile)
            {
                using (var memoryStream = new MemoryStream())
                {
                    snapshot.Screenshot.Save(memoryStream, ImageFormat.Png);
                    byte[] imageBytes = memoryStream.ToArray();
                    snapshot.ScreenshotBase64 = Convert.ToBase64String(imageBytes);
                }

                snapshot.Screenshot = null;
                var img = snapshot.Screenshot;
                string screenshotFileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
                string screenshotFilePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), screenshotFileName);
                img.Save(screenshotFilePath);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(snapshot.ScreenshotFilePath))
                {
                    string screenshotFileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
                    string screenshotFilePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), screenshotFileName);
                    snapshot.Screenshot.Save(screenshotFilePath, ImageFormat.Png);
                    snapshot.ScreenshotFilePath = screenshotFilePath;
                }
            }

            string snapshotJson = JsonConvert.SerializeObject(snapshot);
            string fileName = $"Snapshot-{DateTime.Now.ToFileTime()}.snpt";
            string filePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), fileName);
            File.WriteAllText(filePath, snapshotJson);
        }
    }
}
