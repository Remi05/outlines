using System.Windows;

namespace OutlinesApp
{
    public class DimensionsConfig
    {
        public float DistanceOutlineWidth { get; private set; } = 2.0f;
        public float ElementOutlineWidth { get; private set; } = 2.0f;
        public float DashLength { get; private set; } = 5.0f;
        public int TextRectangleOffset { get; private set; } = 12;
        public Size DistanceRectangleSize { get; private set; } = new Size(50, 22);
        public Size DimensionsRectangleSize { get; private set; } = new Size(85, 22);
    }
}
