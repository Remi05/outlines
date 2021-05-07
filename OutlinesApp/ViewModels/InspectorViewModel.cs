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

        private void OnMouseMoved(int cursorX, int cursorY)
        {
            //OutlinesService.TargetElementAt(new Point(cursorX, cursorY));
        }

        private void OnMouseDown(int cursorX, int cursorY)
        {
            OutlinesService.SelectElementAt(new Point(cursorX, cursorY));
        }

        private void OnKeyDown(int vkCode)
        {

        }

        private void OnKeyUp(int vkCode)
        {

        }
    }
}
