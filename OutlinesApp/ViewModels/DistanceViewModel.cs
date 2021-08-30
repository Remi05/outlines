﻿using System;
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
        private ICoordinateConverter CoordinateConverter { get; set; }
        private IScreenHelper ScreenHelper { get; set; }

        public string DistanceText => $"{DistanceOutline.Distance}";

        public bool IsDashedLine => DistanceOutline.IsAlignmentLine;
        public bool IsTextVisible => !DistanceOutline.IsAlignmentLine && DistanceOutline.Distance != 0;

        public DistanceTextPlacement TextPlacement { get; private set; }

        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }

        public Rect TextContainerRect { get; private set; }

        public DistanceViewModel(DistanceOutline distanceOutline, ICoordinateConverter coordinateConverter, IScreenHelper screenHelper)
        {
            if (distanceOutline == null || coordinateConverter == null)
            {
                throw new ArgumentNullException(distanceOutline == null ? nameof(distanceOutline) : nameof(coordinateConverter));
            }
            DistanceOutline = distanceOutline;
            CoordinateConverter = coordinateConverter;
            ScreenHelper = screenHelper;
            StartPoint = CoordinateConverter.PointFromScreen(distanceOutline.StartPoint);
            EndPoint = CoordinateConverter.PointFromScreen(distanceOutline.EndPoint);
            InitializeTextContainerRect();
        } 

        private void InitializeTextContainerRect()
        {
            // Default to a centered rectangle to the right or below the outline.
            var localMidPoint = CoordinateConverter.PointFromScreen(DistanceOutline.MidPoint);
            var textContainerTopLeft = DistanceOutline.IsVertical
                                     ? new Point(localMidPoint.X, localMidPoint.Y - TextContainerRectHeight / 2)
                                     : new Point(localMidPoint.X - TextContainerRectWidth / 2, localMidPoint.Y);
            TextContainerRect = new Rect(textContainerTopLeft, TextContainerRectSize);
            TextPlacement = DistanceOutline.IsVertical ? DistanceTextPlacement.Right : DistanceTextPlacement.Bottom;

            Rect monitorRect = ScreenHelper.GetDisplayRect(localMidPoint);
            Rect localMonitorRect = CoordinateConverter.RectFromScreen(monitorRect);
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
