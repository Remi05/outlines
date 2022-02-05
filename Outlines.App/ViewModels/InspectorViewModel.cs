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
        private IInspectorStateManager InspectorStateManager { get; set; }

        public bool IsBackdropVisible => InspectorStateManager.IsBackdropVisible;
        public bool IsOverlayVisible => InspectorStateManager.IsOverlayVisible;
        public bool IsPropertiesPanelVisible => InspectorStateManager.IsPropertiesPanelVisible;
        public bool IsTreeViewVisible => InspectorStateManager.IsTreeViewVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public InspectorViewModel(IOutlinesService outlinesService, IGlobalInputListener globalInputListener, IInspectorStateManager inspectorStateManager)
        {
            if (outlinesService == null || globalInputListener == null || inspectorStateManager == null)
            {
                throw new ArgumentNullException(outlinesService == null ? nameof(outlinesService) 
                                              : globalInputListener == null ? nameof(globalInputListener) 
                                              : nameof(inspectorStateManager));
            }
            OutlinesService = outlinesService;
            GlobalInputListener = globalInputListener;
            InspectorStateManager = inspectorStateManager;

            InspectorStateManager.IsOverlayVisibleChanged         += (_) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOverlayVisible)));
            InspectorStateManager.IsPropertiesPanelVisibleChanged += (_) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPropertiesPanelVisible)));
            InspectorStateManager.IsTreeViewVisibleChanged        += (_) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTreeViewVisible)));
            InspectorStateManager.IsBackdropVisibleChanged        += (_) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBackdropVisible)));

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
                InspectorStateManager.IsBackdropVisible = true;
            }
        }

        private void OnKeyUp(int vkCode)
        {
            Key key = KeyInterop.KeyFromVirtualKey(vkCode);
            if (key == Key.LeftCtrl)
            {
                InspectorStateManager.IsBackdropVisible = false;
            }
        }
    }
}
