using System;
using System.Collections.Generic;
using System.Drawing;

namespace Outlines.Core
{
    public delegate void SelectedElementChangedHandler();
    public delegate void TargetElementChangedHandler();

    public interface IOutlinesService
    {
        List<DistanceOutline> DistanceOutlines { get; }
        ElementProperties SelectedElementProperties { get; set; }
        ElementProperties TargetElementProperties { get; set; }

        event SelectedElementChangedHandler SelectedElementChanged;
        event TargetElementChangedHandler TargetElementChanged;

        void SelectElementAt(Point cursorPosition);
        void SelectElementWithNativeHandle(IntPtr nativeHandle);
        void SelectElementWithProperties(ElementProperties properties);
        void TargetElementAt(Point cursorPosition);
        void TargetElementWithNativeHandle(IntPtr nativeHandle);
        void TargetElementWithProperties(ElementProperties properties);
    }
}