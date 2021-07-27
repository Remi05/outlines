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
        private IFolderConfig FolderConfig { get; set; }

        public SnapshotService(IScreenshotService screenshotService, IUiTreeService uiTreeService, IFolderConfig folderConfig)
        {
            if (screenshotService == null || uiTreeService == null || folderConfig == null)
            {
                throw new ArgumentNullException(screenshotService == null ? nameof(screenshotService)
                                              : uiTreeService == null ? nameof(uiTreeService)
                                              : nameof(folderConfig));
            }
            ScreenshotService = screenshotService;
            UiTreeService = uiTreeService;
            FolderConfig = folderConfig;
        }

        public Snapshot TakeSnapshot(ElementProperties elementProperties)
        {
            UiTreeNode subtree = UiTreeService.GetSubTree(elementProperties);
            Image screenshot = ScreenshotService.TakeScreenshot(elementProperties);
            return new Snapshot() { UiTree = subtree, Screenshot = screenshot };
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
            string fileName = $"Snapshot-{DateTime.Now.ToFileTime()}.json";
            string filePath = Path.Combine(FolderConfig.GetSnapshotsFolder(), fileName);
            File.WriteAllText(filePath, snapshotJson);
        }

        public Snapshot LoadSnapshot(string snapshotFilePath)
        {
            if (!File.Exists(snapshotFilePath))
            {
                return null;
            }

            string snapshotJson = File.ReadAllText(snapshotFilePath);
            Snapshot snapshot = JsonConvert.DeserializeObject<Snapshot>(snapshotJson);

            if (!string.IsNullOrWhiteSpace(snapshot.ScreenshotFilePath) && File.Exists(snapshot.ScreenshotFilePath))
            {
                using (var screenshot = Image.FromFile(snapshot.ScreenshotFilePath))
                {
                    snapshot.Screenshot = new Bitmap(screenshot);
                }        
            }

            return snapshot;
        }
    }
}
