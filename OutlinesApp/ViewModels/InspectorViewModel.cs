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

        public event PropertyChangedEventHandler PropertyChanged;

        public InspectorViewModel(IOutlinesService outlinesService, IGlobalInputListener globalInputListener)
        {
            if (outlinesService == null || globalInputListener == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService) : nameof(globalInputListener));
            }
            OutlinesService = outlinesService;
            GlobalInputListener = globalInputListener;

            HoverWatcher targetHoverWatcher = new HoverWatcher(TargetHoverDelayInMs);
            targetHoverWatcher.MouseHovered += OnMouseHovered;
            GlobalInputListener.MouseMoved += targetHoverWatcher.OnMouseMoved;
            GlobalInputListener.MouseDown += OnMouseDown;
            GlobalInputListener.KeyDown += OnKeyDown;
            GlobalInputListener.KeyUp += OnKeyUp;
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
