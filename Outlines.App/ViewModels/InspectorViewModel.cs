using System;
using System.ComponentModel;
using System.Windows.Input;
using Outlines.Core;
using Outlines.Inspection;
using Outlines.App.Services;

namespace Outlines.App.ViewModels
{
    public class InspectorViewModel : INotifyPropertyChanged
    {
        private const int TargetHoverDelayInMs = 75;

        private IOutlinesService OutlinesService { get; set; }
        private IGlobalInputListener GlobalInputListener { get; set; }
        private IInspectorStateManager InspectorManager { get; set; }

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

        public bool IsOverlayVisible => InspectorManager.IsOverlayVisible;
        public bool IsPropertiesPanelVisible => InspectorManager.IsPropertiesPanelVisible;
        public bool IsTreeViewVisible => InspectorManager.IsTreeViewVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public InspectorViewModel(IOutlinesService outlinesService, IGlobalInputListener globalInputListener, IInspectorStateManager inspectorManager)
        {
            if (outlinesService == null || globalInputListener == null || inspectorManager == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService) 
                                              : globalInputListener == null ? nameof(globalInputListener) 
                                              : nameof(inspectorManager));
            }
            OutlinesService = outlinesService;
            GlobalInputListener = globalInputListener;
            InspectorManager = inspectorManager;

            InspectorManager.IsOverlayVisibleChanged         += (_) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOverlayVisible)));
            InspectorManager.IsPropertiesPanelVisibleChanged += (_) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPropertiesPanelVisible)));
            InspectorManager.IsTreeViewVisibleChanged        += (_) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTreeViewVisible)));

            HoverWatcher targetHoverWatcher = new HoverWatcher(TargetHoverDelayInMs);
            targetHoverWatcher.MouseHovered += OnMouseHovered;
            GlobalInputListener.MouseMoved += targetHoverWatcher.OnMouseMoved;
            GlobalInputListener.MouseDown += OnMouseDown;
            GlobalInputListener.KeyDown += OnKeyDown;
            GlobalInputListener.KeyUp += OnKeyUp;
        }

        private void OnMouseHovered(System.Drawing.Point cursorPos)
        {
            OutlinesService.TargetElementAt(cursorPos);
        }

        private void OnMouseDown(System.Drawing.Point cursorPos)
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
