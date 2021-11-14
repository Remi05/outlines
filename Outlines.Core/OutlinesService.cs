using System;
using System.Collections.Generic;
using System.Drawing;

namespace Outlines.Core
{
    public class OutlinesService : IOutlinesService
    {
        private IDistanceOutlinesProvider DistanceOutlinesProvider { get; set; }
        private IElementProvider ElementProvider { get; set; }

        private ElementProperties selectedElementProperties = null;
        public ElementProperties SelectedElementProperties
        {
            get { return selectedElementProperties; }
            set
            {
                if (value != selectedElementProperties)
                {
                    selectedElementProperties = value;
                    UpdateDistanceOutlines();
                    SelectedElementChanged?.Invoke();
                }
            }
        }

        public TextProperties SelectedTextProperties { get; private set; }

        private ElementProperties targetElementProperties = null;
        public ElementProperties TargetElementProperties
        {
            get { return targetElementProperties; }
            set
            {
                if (value != targetElementProperties)
                {
                    targetElementProperties = value;
                    UpdateDistanceOutlines();
                    TargetElementChanged?.Invoke();
                }
            }
        }

        public List<DistanceOutline> DistanceOutlines { get; private set; } = new List<DistanceOutline>();

        public event SelectedElementChangedHandler SelectedElementChanged;
        public event TargetElementChangedHandler TargetElementChanged;

        public OutlinesService(IDistanceOutlinesProvider distanceOutlinesProvider, IElementProvider elementProvider)
        {
            DistanceOutlinesProvider = distanceOutlinesProvider;
            ElementProvider = elementProvider;
        }

        public void SelectElementAt(Point cursorPosition)
        {
            SelectedElementProperties = ElementProvider.TryGetElementFromPoint(cursorPosition);
        }

        public void SelectElementWithNativeHandle(IntPtr nativeHandle)
        {
            SelectedElementProperties = ElementProvider.TryGetElementFromHandle(nativeHandle);
        }

        public void SelectElementWithProperties(ElementProperties properties)
        {
            SelectedElementProperties = properties;
        }

        public void TargetElementAt(Point cursorPosition)
        {
            TargetElementProperties = ElementProvider.TryGetElementFromPoint(cursorPosition);
        }

        public void TargetElementWithNativeHandle(IntPtr nativeHandle)
        {
            TargetElementProperties = ElementProvider.TryGetElementFromHandle(nativeHandle);
        }

        public void TargetElementWithProperties(ElementProperties properties)
        {
            TargetElementProperties = properties;
        }

        private void UpdateDistanceOutlines()
        {
            DistanceOutlines = DistanceOutlinesProvider.GetDistanceOutlines(SelectedElementProperties, TargetElementProperties);
        }
    }
}
