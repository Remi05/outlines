using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class InspectorViewModel : INotifyPropertyChanged
    {
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackdropVisibility)));
                }
            }
        }

        public Visibility BackdropVisibility => IsBackdropVisible ? Visibility.Visible : Visibility.Collapsed;

        private ElementProperties selectedElementProperties;
        public ElementProperties SelectedElementProperties
        {
            get => selectedElementProperties;
            private set
            {
                if (value != selectedElementProperties)
                {
                    selectedElementProperties = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedElementProperties)));
                }
            }
        }

        private ElementProperties targetElementProperties;
        public ElementProperties TargetElementProperties
        {
            get => targetElementProperties;
            private set
            {
                if (value != targetElementProperties)
                {
                    targetElementProperties = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetElementProperties)));
                }
            }
        }

        private List<DistanceOutline> lines;
        public List<DistanceOutline> Lines
        {
            get => lines;
            private set
            {
                lines = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lines)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public InspectorViewModel(IOutlinesService outlinesService, IGlobalInputListener globalInputListener)
        {
            OutlinesService = outlinesService;
            GlobalInputListener = globalInputListener;

            if (OutlinesService != null)
            {
                OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
                OutlinesService.TargetElementChanged += OnTargetElementChanged;
                SelectedElementProperties = OutlinesService.SelectedElementProperties;
                TargetElementProperties = OutlinesService.TargetElementProperties;
                Lines = OutlinesService.DistanceOutlines;

                if (GlobalInputListener != null)
                {
                    GlobalInputListener.MouseMoved += OnMouseMoved;
                    GlobalInputListener.MouseDown += OnMouseDown;
                    GlobalInputListener.KeyDown += OnKeyDown;
                    GlobalInputListener.KeyUp += OnKeyUp;
                }
            }
        }

        private void OnSelectedElementChanged()
        {
            SelectedElementProperties = OutlinesService.SelectedElementProperties;
            Lines = OutlinesService.DistanceOutlines;
        }

        private void OnTargetElementChanged()
        {
            TargetElementProperties = OutlinesService.TargetElementProperties;
            Lines = OutlinesService.DistanceOutlines;
        }

        private void OnMouseMoved(Point cursorPoint)
        {
            //OutlinesService.TargetElementAt(cursorPos);
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
