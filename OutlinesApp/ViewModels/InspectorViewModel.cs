using System.Collections.Generic;
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
            get => isBackdropVisible;
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
            private set
            {
                if (value != isOverlayVisible)
                {
                    isOverlayVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOverlayVisible)));
                }
            }
        }

        private bool isPropertiesPanelVisible = true;
        public bool IsPropertiesPanelVisible
        {
            get => isPropertiesPanelVisible;
            private set
            {
                if (value != isPropertiesPanelVisible)
                {
                    isPropertiesPanelVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPropertiesPanelVisible)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public InspectorViewModel(IOutlinesService outlinesService, IGlobalInputListener globalInputListener)
        {
            OutlinesService = outlinesService;
            GlobalInputListener = globalInputListener;

            HoverWatcher targetHoverWatcher = new HoverWatcher(TargetHoverDelayInMs);
            targetHoverWatcher.MouseHovered += OnMouseHovered;
            GlobalInputListener.MouseMoved += targetHoverWatcher.OnMouseMoved;
            GlobalInputListener.MouseDown += OnMouseDown;
            GlobalInputListener.KeyDown += OnKeyDown;
            GlobalInputListener.KeyUp += OnKeyUp;
        }

        public void ToggleOverlay()
        {
            IsOverlayVisible = !IsOverlayVisible;
        }

        public void TogglePropertiesPanel()
        {
            IsPropertiesPanelVisible = !IsPropertiesPanelVisible;
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
