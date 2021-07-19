using System.Collections.Generic;
using System.Windows;

namespace Outlines
{
    public delegate void SelectedElementChangedHandler();
    public delegate void TargetElementChangedHandler();

    public interface IOutlinesService
    {
        List<DistanceOutline> DistanceOutlines { get; }
        ElementProperties SelectedElementProperties { get; set; }
        TextProperties SelectedTextProperties { get; }
        ElementProperties TargetElementProperties { get; set; }

        event SelectedElementChangedHandler SelectedElementChanged;
        event TargetElementChangedHandler TargetElementChanged;

        void SelectElementAt(Point cursorPosition);
        void SelectElementWithProperties(ElementProperties properties);
        void TargetElementAt(Point cursorPosition);
        void TargetElementWithProperties(ElementProperties properties);
    }
}