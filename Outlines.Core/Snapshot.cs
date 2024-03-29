﻿using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Outlines.Core
{
    public class Snapshot
    {
        public double ScaleFactor { get; set; }
        public string ScreenshotFilePath { get; set; }
        public string ScreenshotBase64 { get; set; }
        public CachedUITreeNode UITree { get; set; }

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
            if (screenshot != null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(ScreenshotBase64))
            {
                byte[] bytes = Convert.FromBase64String(ScreenshotBase64);
                using (var memoryStream = new MemoryStream(bytes))
                {
                    screenshot = Image.FromStream(memoryStream);
                }
            }
            else if (!string.IsNullOrWhiteSpace(ScreenshotFilePath) && File.Exists(ScreenshotFilePath))
            {
                using (var screenshotImage = Image.FromFile(ScreenshotFilePath))
                {
                    screenshot = new Bitmap(screenshotImage);
                }
            }
        }

        public static Snapshot LoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                return null;
            }
            string snapshotJson = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Snapshot>(snapshotJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }
    }
}
