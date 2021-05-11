using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class OutlinesViewModel : INotifyPropertyChanged
    {
        private Visual RootVisual { get; set; }
        private IOutlinesService OutlinesService { get; set; }

        //public DoubleCollection DashPattern = new DoubleCollection() { 5.0, 5.0 };

        public ObservableCollection<DistanceOutline> DistanceOutlines { get; private set; } = new ObservableCollection<DistanceOutline>();

        private Rect selectedElementRect = Rect.Empty;
        public Rect SelectedElementRect
        {
            get => selectedElementRect;
            private set
            {
                if (value != selectedElementRect)
                {
                    selectedElementRect = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedElementRect)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelectedElementRectVisible)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTargetElementRectVisible)));
                }
            }
        }

        private Rect targetElementRect = Rect.Empty;
        public Rect TargetElementRect
        {
            get => targetElementRect;
            private set
            {
                if (value != targetElementRect)
                {
                    targetElementRect = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetElementRect)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTargetElementRectVisible)));
                }
            }
        }

        public bool IsSelectedElementRectVisible => SelectedElementRect != Rect.Empty;
        public bool IsTargetElementRectVisible => TargetElementRect != Rect.Empty && TargetElementRect != SelectedElementRect;

        public event PropertyChangedEventHandler PropertyChanged;

        public OutlinesViewModel(Visual rootVisual, IOutlinesService outlinesService)
        {
            RootVisual = rootVisual;
            OutlinesService = outlinesService;
            OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
            OutlinesService.TargetElementChanged += OnTargetElementChanged;
        }

        private void OnSelectedElementChanged()
        {
            if (OutlinesService.SelectedElementProperties != null)
            {
                RootVisual.Dispatcher.Invoke(() => 
                {
                    SelectedElementRect = RectFromScreen(OutlinesService.SelectedElementProperties.BoundingRect);
                });
            }
            UpdateDistanceOutlines();
        }
        
        private void OnTargetElementChanged()
        {
            if (OutlinesService.TargetElementProperties != null)
            {
                RootVisual.Dispatcher.Invoke(() => 
                {
                    TargetElementRect = RectFromScreen(OutlinesService.TargetElementProperties.BoundingRect);
                });
            }
            UpdateDistanceOutlines();
        }

        private void UpdateDistanceOutlines()
        {
            RootVisual.Dispatcher.Invoke(() => 
            {
                DistanceOutlines.Clear();
                OutlinesService.DistanceOutlines.ForEach((distanceOutline) => 
                {
                    var start = RootVisual.PointFromScreen(distanceOutline.StartPoint);
                    var end = RootVisual.PointFromScreen(distanceOutline.EndPoint);
                    DistanceOutlines.Add(new DistanceOutline(start, end, distanceOutline.IsDashedLine));
                });
            });
        }

        private Rect RectFromScreen(Rect screenRect)
        {
            Point localPosition = RootVisual.PointFromScreen(screenRect.TopLeft);
            Matrix transformFromDevice = PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformFromDevice;
            Vector sizeVector = new Vector(screenRect.Width, screenRect.Height);
            Vector localSizeVector = transformFromDevice.Transform(sizeVector);
            return new Rect(localPosition, new Size(localSizeVector.X, localSizeVector.Y));
        }
    }
}
