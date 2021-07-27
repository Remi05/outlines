using System;
using System.IO;

namespace Outlines
{
    public class FolderConfig : IFolderConfig
    {
        private readonly string ImagesRootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Outlines");
        private string ScreenshotsFolderPath { get; set; }
        private string SnapshotsFolderPath { get; set; }

        public FolderConfig()
        {
            ScreenshotsFolderPath = Path.Combine(ImagesRootFolder, "screenshots");
            SnapshotsFolderPath = Path.Combine(ImagesRootFolder, "snapshots");
        }

        public string GetScreenshotsFolderPath()
        {
            EnsureFolder(ScreenshotsFolderPath);
            return ScreenshotsFolderPath;
        }

        public string GetSnapshotsFolder()
        {
            EnsureFolder(SnapshotsFolderPath);
            return SnapshotsFolderPath;
        }

        private void EnsureFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}
