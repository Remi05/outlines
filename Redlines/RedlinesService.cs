using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;

namespace Redlines
{
    public delegate void SelectedElementChangedHandler();
    public delegate void TargetElementChangedHandler();

    public class RedlinesService
    {
        private DistanceOutlinesProvider DistanceOutlinesProvider { get; set; } = new DistanceOutlinesProvider();
        private ElementPropertiesProvider ElementPropertiesProvider { get; set; } = new ElementPropertiesProvider();
        private TextPropertiesProvider TextPropertiesProvider { get; set; } = new TextPropertiesProvider();

        private AutomationElement selectedElement = null;
        private AutomationElement SelectedElement
        {
            get { return selectedElement;  }
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

        public SelectedElementChangedHandler SelectedElementChanged;
        public TargetElementChangedHandler TargetElementChanged;

        public void TargetElementAt(Point cursorPosition)
        {
            TargetElement = TryGetElementFromPoint(cursorPosition);
        }

        public void SelectElementAt(Point cursorPosition)
        {
            SelectedElement = TryGetElementFromPoint(cursorPosition);
        }

        private AutomationElement TryGetElementFromPoint(Point point)
        {
            try
            {
                return AutomationElement.FromPoint(point);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void UpdateDistanceOutlines()
        {
            DistanceOutlines = DistanceOutlinesProvider.GetDistanceOutlines(SelectedElementProperties, TargetElementProperties);
        }
    }
}
