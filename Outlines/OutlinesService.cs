using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public class OutlinesService : IOutlinesService
    {
        private IDistanceOutlinesProvider DistanceOutlinesProvider { get; set; }
        private IElementProvider ElementProvider { get; set; }
        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }
        private ITextPropertiesProvider TextPropertiesProvider { get; set; }

        private AutomationElement selectedElement = null;
        private AutomationElement SelectedElement
        {
            get { return selectedElement; }
            set
            {
                selectedElement = value;
                SelectedTextProperties = TextPropertiesProvider.GetTextProperties(SelectedElement);
                SelectedElementProperties = ElementPropertiesProvider.GetElementProperties(SelectedElement);
            }
        }

        private AutomationElement targetElement = null;
        private AutomationElement TargetElement
        {
            get { return targetElement; }
            set
            {
                targetElement = value;
                TargetElementProperties = ElementPropertiesProvider.GetElementProperties(TargetElement);
            }
        }

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

        public OutlinesService(IDistanceOutlinesProvider distanceOutlinesProvider, IElementProvider elementProvider,
                               IElementPropertiesProvider elementPropertiesProvider, ITextPropertiesProvider textPropertiesProvider)
        {
            DistanceOutlinesProvider = distanceOutlinesProvider;
            ElementProvider = elementProvider;
            ElementPropertiesProvider = elementPropertiesProvider;
            TextPropertiesProvider = textPropertiesProvider;
        }

        public void SelectElementAt(Point cursorPosition)
        {
            SelectedElement = ElementProvider.TryGetElementFromPoint(cursorPosition);
        }

        public void SelectElementWithProperties(ElementProperties properties)
        {
            SelectedElement = ElementProvider.TryGetElementFromProperties(properties);
        }

        public void TargetElementAt(Point cursorPosition)
        {
            TargetElement = ElementProvider.TryGetElementFromPoint(cursorPosition);
        }

        public void TargetElementWithProperties(ElementProperties properties)
        {
            TargetElement = ElementProvider.TryGetElementFromProperties(properties);
        }

        private void UpdateDistanceOutlines()
        {
            DistanceOutlines = DistanceOutlinesProvider.GetDistanceOutlines(SelectedElementProperties, TargetElementProperties);
        }
    }
}
