using System;
using System.Drawing;
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

        public Snapshot TakeSnapshot(ElementProperties elementProperties)
        {
            UiTreeNode subtree = UiTreeService.GetSubTree(elementProperties);
            Image screenshot = ScreenshotService.TakeScreenshot(elementProperties);
            double scaleFactor = ScreenHelper.GetDisplayScaleFactor();
            return new Snapshot() { UiTree = subtree, Screenshot = screenshot, ScaleFactor = scaleFactor };
        }

        public void SaveSnapshot(Snapshot snapshot)
        {
            if (string.IsNullOrWhiteSpace(snapshot.ScreenshotFilePath))
            {
                string screenshotFileName = $"Screenshot-{DateTime.Now.ToFileTime()}.png";
                string screenshotFilePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), screenshotFileName);
                snapshot.Screenshot.Save(screenshotFilePath);
                snapshot.ScreenshotFilePath = screenshotFilePath;
            }

            string snapshotJson = JsonConvert.SerializeObject(snapshot);
            string fileName = $"Snapshot-{DateTime.Now.ToFileTime()}.snpt";
            string filePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), fileName);
            File.WriteAllText(filePath, snapshotJson);
        }
    }
}
