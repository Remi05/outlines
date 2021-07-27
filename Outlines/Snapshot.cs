using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace Outlines
{
    public class Snapshot
    {
        public UiTreeNode UiTree { get; set; }
        public string ScreenshotFilePath { get; set; }

        [JsonIgnore]
        private Image Screenshot { get; set; }

        public Snapshot(UiTreeNode uiTreeRoot, string screenshotFilePath)
        {
            UiTree = uiTreeRoot;
            ScreenshotFilePath = screenshotFilePath;
        }

        public Snapshot(UiTreeNode uiTreeRoot, Image screenshot)
        {
            UiTree = uiTreeRoot;
            Screenshot = screenshot;
        }

        public Image GetScreenshot()
        {
            if (Screenshot == null)
            {
                if (ScreenshotFilePath == null || !File.Exists(ScreenshotFilePath))
                {
                    return null;
                }
                Screenshot = Image.FromFile(ScreenshotFilePath);
            }
            return Screenshot;
        }
    }
}
