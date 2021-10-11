using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class InspectorViewModel : INotifyPropertyChanged
    {
        private const int TargetHoverDelayInMs = 75;

        private IOutlinesService OutlinesService { get; set; }
        private IGlobalInputListener GlobalInputListener { get; set; }
        private ISnapshotService SnapshotService { get; set; }
        private RectangleSelectionViewModel RectangleSelectionViewModel { get; set; }

        private bool isBackdropVisible = false;
        public bool IsBackdropVisible
        {
            get => isBackdropVisible && IsOverlayVisible;
            private set
            {
                if (value != isBackdropVisible)
                {
                    isBackdropVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBackdropVisible)));
                }
            }
        }

        private bool isOverlayVisible = true;
        public bool IsOverlayVisible
        {
            get => isOverlayVisible;
            set
            {
                if (value != isOverlayVisible)
                {
                    isOverlayVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOverlayVisible)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBackdropVisible)));
                }
            }
        }

        private bool isPropertiesPanelVisible = true;
        public bool IsPropertiesPanelVisible
        {
            get => isPropertiesPanelVisible;
            set
            {
                if (value != isPropertiesPanelVisible)
                {
                    isPropertiesPanelVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPropertiesPanelVisible)));
                }
            }
        }

        private bool isTreeViewVisible = true;
        public bool IsTreeViewVisible
        {
            get => isTreeViewVisible;
            set
            {
                if (value != isTreeViewVisible)
                {
                    isTreeViewVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTreeViewVisible)));
                }
            }
        }
  
        private bool isTakingRectangleSnapshot = false;
        public bool IsTakingRectangleSnapshot
        {
            get => isTakingRectangleSnapshot;
            set
            {
                if (value != isTakingRectangleSnapshot)
                {
                    isTakingRectangleSnapshot = value;
                    if (isTakingRectangleSnapshot)
                    {
                        RectangleSelectionViewModel.RectangleSelected += OnSnapshotRectangleSelected;
                    }
                    else
                    {
                        RectangleSelectionViewModel.RectangleSelected -= OnSnapshotRectangleSelected;
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTakingRectangleSnapshot)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public InspectorViewModel(IOutlinesService outlinesService, IGlobalInputListener globalInputListener, ISnapshotService snapshotService, RectangleSelectionViewModel rectangleSelectionViewModel)
        {
            if (outlinesService == null || globalInputListener == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService) : nameof(globalInputListener));
            }
            OutlinesService = outlinesService;
            GlobalInputListener = globalInputListener;
            SnapshotService = snapshotService;
            RectangleSelectionViewModel = rectangleSelectionViewModel;

            HoverWatcher targetHoverWatcher = new HoverWatcher(TargetHoverDelayInMs);
            targetHoverWatcher.MouseHovered += OnMouseHovered;
            GlobalInputListener.MouseMoved += targetHoverWatcher.OnMouseMoved;
            GlobalInputListener.MouseDown += OnMouseDown;
            GlobalInputListener.KeyDown += OnKeyDown;
            GlobalInputListener.KeyUp += OnKeyUp;
        }

        private void OnSnapshotRectangleSelected(Rect snapshotRectangle)
        {
            IsTakingRectangleSnapshot = false;
            // SnapshotService.TakeSnapshot(snapshotRectangle);
        }

        private void OnMouseHovered(Point cursorPos)
        {
            OutlinesService.TargetElementAt(cursorPos);
        }

        private void OnMouseDown(Point cursorPos)
        {
            OutlinesService.SelectElementAt(cursorPos);
        }

        private void OnKeyDown(int vkCode)
        {
            Key key = KeyInterop.KeyFromVirtualKey(vkCode);
            if (key == Key.LeftCtrl)
            {
                IsBackdropVisible = true;
            }
        }

        private void OnKeyUp(int vkCode)
        {
            Key key = KeyInterop.KeyFromVirtualKey(vkCode);
            if (key == Key.LeftCtrl)
            {
                IsBackdropVisible = false;
            }
        }
    }
}
