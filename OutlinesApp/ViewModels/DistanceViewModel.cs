using System.Windows;
using Outlines;
using OutlinesApp.Services;

namespace OutlinesApp.ViewModels
{
    public class DistanceViewModel
    {
        private const int TextContainerRectWidth = 75;
        private const int TextContainerRectHeight = 25;

        private DistanceOutline DistanceOutline { get; set; }
        private IScreenHelper ScreenHelper { get; set; }

        public string DistanceText => $"{DistanceOutline.Distance}";

        public bool IsDashedLine => DistanceOutline.IsAlignmentLine;
        public bool IsTextVisible => !DistanceOutline.IsAlignmentLine && DistanceOutline.Distance != 0;
        public bool IsVertical => DistanceOutline.IsVertical;

        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }

        public Rect TextContainerRect { get; private set; }

        public DistanceViewModel(DistanceOutline distanceOutline, IScreenHelper screenHelper)
        {
            DistanceOutline = distanceOutline;
            ScreenHelper = screenHelper;
            StartPoint = ScreenHelper.PointFromScreen(distanceOutline.StartPoint);
            EndPoint = ScreenHelper.PointFromScreen(distanceOutline.EndPoint);
            InitializeTextContainerRect();
        } 

        private void InitializeTextContainerRect()
        {
            var localMidPoint = ScreenHelper.PointFromScreen(DistanceOutline.MidPoint);
            var textContainerTopLeft = DistanceOutline.IsVertical
                                     ? new Point(localMidPoint.X, localMidPoint.Y - TextContainerRectHeight / 2)
                                     : new Point(localMidPoint.X - TextContainerRectWidth / 2, localMidPoint.Y);
            TextContainerRect = new Rect(textContainerTopLeft, new Size(TextContainerRectWidth, TextContainerRectHeight));
        }
    }
}
