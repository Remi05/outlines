using System;
using System.Drawing;

namespace Outlines.ImageProcessing
{
    public class TwoToneFilter
    {
        private const int DefaultBrightnessThreshold = 128;
        private Color BaseColor { get; set; } = Color.Black;
        private Color HighlightColor { get; set; } = Color.White;

        public Bitmap Apply(Bitmap image, double brightnessThreshold = DefaultBrightnessThreshold)
        {
            var imageWithFilter = new Bitmap(image);

            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    Color srcPixel = image.GetPixel(x, y);
                    double brightness = GetPixelBrightness(srcPixel);
                    Color dstPixel = brightness < brightnessThreshold ? BaseColor : HighlightColor;
                    imageWithFilter.SetPixel(x, y, dstPixel);
                }
            }

            return imageWithFilter;
        }

        private double GetPixelBrightness(Color pixel)
        {
            return Math.Sqrt(Math.Pow(pixel.R, 2) + Math.Pow(pixel.G, 2) + Math.Pow(pixel.B, 2));
        }
    }
}
