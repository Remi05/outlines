using System.Drawing;
using Newtonsoft.Json;

namespace Outlines
{
    public class Snapshot
    {
        public UiTreeNode UiTree { get; set; }
        public string ScreenshotFilePath { get; set; }

        [JsonIgnore]
        public Image Screenshot { get; set; }
    }
}
