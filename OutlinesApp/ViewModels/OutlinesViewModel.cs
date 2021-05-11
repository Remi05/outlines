using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class OutlinesViewModel : INotifyPropertyChanged
    {
        private Dispatcher Dispatcher { get; set; }
        private IScreenHelper ScreenHelper { get; set; }
        private IOutlinesService OutlinesService { get; set; }

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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedElementDimensionsViewModel)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetElementDimensionsViewModel)));
                }
            }
        }

        public bool IsSelectedElementRectVisible => SelectedElementRect != Rect.Empty;
        public bool IsTargetElementRectVisible => TargetElementRect != Rect.Empty && TargetElementRect != SelectedElementRect;

        public DimensionViewModel SelectedElementDimensionsViewModel => OutlinesService.SelectedElementProperties == null ? null : new DimensionViewModel(OutlinesService.SelectedElementProperties, ScreenHelper);
        public DimensionViewModel TargetElementDimensionsViewModel => OutlinesService.TargetElementProperties == null ? null : new DimensionViewModel(OutlinesService.TargetElementProperties, ScreenHelper);

        public event PropertyChangedEventHandler PropertyChanged;

        public OutlinesViewModel(Dispatcher dispatcher, IScreenHelper screenHelper, IOutlinesService outlinesService)
        {
            Dispatcher = dispatcher;
            ScreenHelper = screenHelper;
            OutlinesService = outlinesService;
            OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
            OutlinesService.TargetElementChanged += OnTargetElementChanged;
        }

        private void OnSelectedElementChanged()
        {
            if (OutlinesService.SelectedElementProperties != null)
            {
                Dispatcher.Invoke(() => 
                {
                    SelectedElementRect = ScreenHelper.RectFromScreen(OutlinesService.SelectedElementProperties.BoundingRect);
                });
            }
            UpdateDistanceOutlines();
        }
        
        private void OnTargetElementChanged()
        {
            if (OutlinesService.TargetElementProperties != null)
            { 
                Dispatcher.Invoke(() => 
                {
                    TargetElementRect = ScreenHelper.RectFromScreen(OutlinesService.TargetElementProperties.BoundingRect);
                });
            }
            UpdateDistanceOutlines();
        }

        private void UpdateDistanceOutlines()
        {
            Dispatcher.Invoke(() => 
            {
                DistanceOutlines.Clear();
                OutlinesService.DistanceOutlines.ForEach((distanceOutline) => 
                {
                    var start = ScreenHelper.PointFromScreen(distanceOutline.StartPoint);
                    var end = ScreenHelper.PointFromScreen(distanceOutline.EndPoint);
                    DistanceOutlines.Add(new DistanceOutline(start, end, distanceOutline.IsDashedLine));
                });
            });
        }
    }
}
