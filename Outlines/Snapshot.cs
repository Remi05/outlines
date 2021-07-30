using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace Outlines
{
    public class Snapshot
    {
        public UiTreeNode UiTree { get; set; }
        public string ScreenshotFilePath { get; set; }

        private Image screenshot = null;

        [JsonIgnore]
        public Image Screenshot
        {
            get
            {
                EnsureScreenshot();
                return screenshot;
            }
            set
            {
                screenshot = value;
            }
        }

        private void EnsureScreenshot()
        {
            if (screenshot != null || string.IsNullOrWhiteSpace(ScreenshotFilePath) || !File.Exists(ScreenshotFilePath))
            {
                return;
            }

            using (var screenshotImage = Image.FromFile(ScreenshotFilePath))
            {
                screenshot = new Bitmap(screenshot);
            }
        }

        public static Snapshot LoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                return null;
            }
            string snapshotJson = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Snapshot>(snapshotJson);
        }
    }
}
