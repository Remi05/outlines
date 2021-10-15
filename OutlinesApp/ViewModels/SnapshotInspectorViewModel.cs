using System;
using System.ComponentModel;
using Outlines.Core;

namespace OutlinesApp.ViewModels
{
    public class SnapshotInspectorViewModel : INotifyPropertyChanged
    {
        private IOutlinesService OutlinesService { get; set; }
        private ICoordinateConverter CoordinateConverter { get; set; }

        private Snapshot snapshot = null;
        public Snapshot Snapshot
        {
            get => snapshot;
            set
            {
                snapshot = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Snapshot)));
            }
        }

        private float screenshotScaleFactor = 1.0f;
        public float ScreenshotScaleFactor
        {
            get => screenshotScaleFactor;
            private set
            {
                if (value != screenshotScaleFactor)
                {
                    screenshotScaleFactor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScreenshotScaleFactor)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SnapshotInspectorViewModel(IOutlinesService outlinesService, ICoordinateConverter coordinateConverter)
        {
            if (outlinesService == null)
            {
                throw new ArgumentNullException(nameof(outlinesService));
            }
            OutlinesService = outlinesService;
            CoordinateConverter = coordinateConverter;
        }

        public void OnMouseMove(System.Drawing.Point cursorPos)
        {
            OutlinesService.TargetElementAt(CoordinateConverter.PointToScreen(cursorPos));
        }

        public void OnMouseDown(System.Drawing.Point cursorPos)
        {
            OutlinesService.SelectElementAt(CoordinateConverter.PointToScreen(cursorPos));
        }

        public void OnMouseWheelScroll(int scrollDelta)
        {
            const float minScaleFactor = 0.001f;
            const float scrollSensitity = 1000.0f;
            float scaleMultiplier = Math.Max((1.0f - scrollDelta / scrollSensitity), minScaleFactor);
            ScreenshotScaleFactor *= scaleMultiplier;
        }
    }
}
