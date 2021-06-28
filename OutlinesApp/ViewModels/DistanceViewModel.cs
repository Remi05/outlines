using System;
using System.Windows;
using Outlines;
using OutlinesApp.Services;

namespace OutlinesApp.ViewModels
{
    public enum DistanceTextPlacement { Left, Right, Top, Bottom };

    public class DistanceViewModel
    {
        private const int TextContainerRectWidth = 60;
        private const int TextContainerRectHeight = 35;
        private readonly Size TextContainerRectSize = new Size(TextContainerRectWidth, TextContainerRectHeight);

        private DistanceOutline DistanceOutline { get; set; }
        private IScreenHelper ScreenHelper { get; set; }

        public string DistanceText => $"{DistanceOutline.Distance}";

        public bool IsDashedLine => DistanceOutline.IsAlignmentLine;
        public bool IsTextVisible => !DistanceOutline.IsAlignmentLine && DistanceOutline.Distance != 0;

        public DistanceTextPlacement TextPlacement { get; private set; }

        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }

        public Rect TextContainerRect { get; private set; }

        public DistanceViewModel(DistanceOutline distanceOutline, IScreenHelper screenHelper)
        {
            if (distanceOutline == null || screenHelper == null)
            {
                throw new ArgumentNullException(distanceOutline == null ? nameof(distanceOutline) : nameof(screenHelper));
            }
            DistanceOutline = distanceOutline;
            ScreenHelper = screenHelper;
            StartPoint = ScreenHelper.PointFromScreen(distanceOutline.StartPoint);
            EndPoint = ScreenHelper.PointFromScreen(distanceOutline.EndPoint);
            InitializeTextContainerRect();
        } 

        private void InitializeTextContainerRect()
        {
            // Default to a centered rectangle to the right or below the outline.
            var localMidPoint = ScreenHelper.PointFromScreen(DistanceOutline.MidPoint);
            var textContainerTopLeft = DistanceOutline.IsVertical
                                     ? new Point(localMidPoint.X, localMidPoint.Y - TextContainerRectHeight / 2)
                                     : new Point(localMidPoint.X - TextContainerRectWidth / 2, localMidPoint.Y);
            TextContainerRect = new Rect(textContainerTopLeft, TextContainerRectSize);
            TextPlacement = DistanceOutline.IsVertical ? DistanceTextPlacement.Right : DistanceTextPlacement.Bottom;

            Rect monitorRect = ScreenHelper.GetMonitorRect(localMidPoint);
            Rect localMonitorRect = ScreenHelper.RectFromScreen(monitorRect);
            if (!localMonitorRect.Contains(TextContainerRect))
            {
                // If the text is outside the screen when shown to the right or below, try on the left or above.
                textContainerTopLeft = DistanceOutline.IsVertical 
                                     ? new Point(localMidPoint.X - TextContainerRectWidth, localMidPoint.Y - TextContainerRectHeight / 2)
                                     : new Point(localMidPoint.X - TextContainerRectWidth / 2, localMidPoint.Y - TextContainerRectHeight);
                TextContainerRect = new Rect(textContainerTopLeft, TextContainerRectSize);
                TextPlacement = DistanceOutline.IsVertical ? DistanceTextPlacement.Left : DistanceTextPlacement.Top;
            }
        }
    }
}
