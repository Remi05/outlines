using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;

namespace Outlines.Core
{
    public class SnapshotService : ISnapshotService
    {
        private IScreenshotService ScreenshotService { get; set; }
        private IUITreeService UITreeService { get; set; }
        private IScreenHelper ScreenHelper { get; set; }
        private IFolderConfig FolderConfig { get; set; }
        private bool ShouldSaveAsSingleFile { get; set; }

        public SnapshotService(IScreenshotService screenshotService, IUITreeService uiTreeService, IScreenHelper screenHelper, IFolderConfig folderConfig, bool shouldSaveAsSingleFile = true)
        {
            if (screenshotService == null || uiTreeService == null || screenHelper ==null || folderConfig == null)
            {
                throw new ArgumentNullException(screenshotService == null ? nameof(screenshotService)
                                              : uiTreeService == null ? nameof(uiTreeService)
                                              : screenHelper == null ? nameof(screenHelper)
                                              : nameof(folderConfig));
            }
            ScreenshotService = screenshotService;
            UITreeService = uiTreeService;
            ScreenHelper = screenHelper;
            FolderConfig = folderConfig;
            ShouldSaveAsSingleFile = shouldSaveAsSingleFile;
        }

        public Snapshot TakeSnapshot(Rectangle bounds)
        {
            UITreeNode subtree = UITreeService.GetSubTreeInBounds(bounds);
            Image screenshot = ScreenshotService.TakeScreenshot(bounds);
            double scaleFactor = ScreenHelper.GetDisplayScaleFactor();
            return new Snapshot() { UITree = subtree, Screenshot = screenshot, ScaleFactor = scaleFactor };
        }

        public Snapshot TakeSnapshot(ElementProperties elementProperties)
        {
            UITreeNode subtree = UITreeService.GetSubTree(elementProperties);
            Image screenshot = ScreenshotService.TakeScreenshot(elementProperties);
            double scaleFactor = ScreenHelper.GetDisplayScaleFactor();
            return new Snapshot() { UITree = subtree, Screenshot = screenshot, ScaleFactor = scaleFactor };
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

            string snapshotJson = JsonSerializer.Serialize(snapshot);
            string fileName = $"Snapshot-{DateTime.Now.ToFileTime()}.snpt";
            string filePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), fileName);
            File.WriteAllText(filePath, snapshotJson);
        }
    }
}
