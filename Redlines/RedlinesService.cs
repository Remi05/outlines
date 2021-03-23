using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;

namespace Redlines
{
    public class RedlinesService
    {
        private DistanceOutlinesProvider DistanceOutlinesProvider { get; set; } = new DistanceOutlinesProvider();
        private ElementPropertiesProvider ElementPropertiesProvider { get; set; } = new ElementPropertiesProvider();
        private TextPropertiesProvider TextPropertiesProvider { get; set; } = new TextPropertiesProvider();

        private AutomationElement selectedElement;
        private AutomationElement SelectedElement
        {
            get { return selectedElement;  }
            set
            {
                selectedElement = value;
                SelectedElementProperties = ElementPropertiesProvider.GetElementProperties(SelectedElement);
                UpdateDistanceOutlines();
            }
        }

        private AutomationElement targetElement;
        private AutomationElement TargetElement
        {
            get { return targetElement; }
            set
            {
                targetElement = value;
                TargetElementProperties = ElementPropertiesProvider.GetElementProperties(TargetElement);
                UpdateDistanceOutlines();
            }
        }

        public ElementProperties SelectedElementProperties { get; private set; } = null;
        public ElementProperties TargetElementProperties { get; private set; } = null;
        public List<DistanceOutline> DistanceOutlines { get; private set; } = new List<DistanceOutline>();


        public void OnCursorMove(Point cursorPosition)
        {
            TargetElement = AutomationElement.FromPoint(cursorPosition);
        }

        public void OnCursorDown(Point cursorPosition)
        {
            SelectedElement = AutomationElement.FromPoint(cursorPosition);
        }

        private void UpdateDistanceOutlines()
        {
            DistanceOutlines = DistanceOutlinesProvider.GetDistanceOutlines(SelectedElementProperties, TargetElementProperties);
        }
    }
}
