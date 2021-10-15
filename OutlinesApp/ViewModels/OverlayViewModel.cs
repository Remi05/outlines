using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Outlines.Core;
using Outlines.Inspection;
using OutlinesApp.Services;

namespace OutlinesApp.ViewModels
{
    public class OverlayViewModel : INotifyPropertyChanged
    {
        private Dispatcher Dispatcher { get; set; }
        private ICoordinateConverter CoordinateConverter { get; set; }
        private IOutlinesService OutlinesService { get; set; }
        private IScreenHelper ScreenHelper { get; set; }

        public ObservableCollection<DistanceViewModel> DistanceOutlines { get; private set; } = new ObservableCollection<DistanceViewModel>();

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

        public DimensionsViewModel SelectedElementDimensionsViewModel => OutlinesService.SelectedElementProperties == null ? null : new DimensionsViewModel(OutlinesService.SelectedElementProperties, CoordinateConverter, ScreenHelper);
        public DimensionsViewModel TargetElementDimensionsViewModel => OutlinesService.TargetElementProperties == null ? null : new DimensionsViewModel(OutlinesService.TargetElementProperties, CoordinateConverter, ScreenHelper);

        public event PropertyChangedEventHandler PropertyChanged;

        public OverlayViewModel(Dispatcher dispatcher, IOutlinesService outlinesService, ICoordinateConverter coordinateConverter, IScreenHelper screenHelper)
        {
            if (dispatcher == null || outlinesService == null || coordinateConverter == null)
            {
                throw new ArgumentNullException(dispatcher == null ? nameof(dispatcher) : outlinesService == null ? nameof(outlinesService) : nameof(coordinateConverter));
            }
            Dispatcher = dispatcher;
            CoordinateConverter = coordinateConverter;
            ScreenHelper = screenHelper;
            OutlinesService = outlinesService;
            OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
            OutlinesService.TargetElementChanged += OnTargetElementChanged;
        }

        private void OnSelectedElementChanged()
        {
            Dispatcher.Invoke(() => 
            {
                SelectedElementRect = OutlinesService.SelectedElementProperties != null
                                    ? CoordinateConverter.RectFromScreen(OutlinesService.SelectedElementProperties.BoundingRect).ToWindowsRect()
                                    : Rect.Empty;

            });  
            UpdateDistanceOutlines();
        }
        
        private void OnTargetElementChanged()
        {    
            Dispatcher.Invoke(() => 
            {
                TargetElementRect = OutlinesService.TargetElementProperties != null 
                                    ? CoordinateConverter.RectFromScreen(OutlinesService.TargetElementProperties.BoundingRect).ToWindowsRect()
                                    : Rect.Empty;
            });
            UpdateDistanceOutlines();
        }

        private void UpdateDistanceOutlines()
        {
            Dispatcher.Invoke(() => 
            {
                DistanceOutlines.Clear();
                OutlinesService.DistanceOutlines.ForEach(distanceOutline => DistanceOutlines.Add(new DistanceViewModel(distanceOutline, CoordinateConverter, ScreenHelper)));
            });
        }
    }
}
