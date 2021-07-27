using System.Windows;
using System.Windows.Automation;
using Newtonsoft.Json;

namespace Outlines
{
    
    public class ElementProperties
    {
        public string Name { get; set; }
        public string ControlType { get; set; }
        public Rect BoundingRect { get; set; }

        [JsonIgnore]
        internal AutomationElement Element { get; set; }

        public override bool Equals(object obj)
        {
            ElementProperties otherProperties = obj as ElementProperties;
            return otherProperties != null && BoundingRect.Equals(otherProperties.BoundingRect);
        }

        public override int GetHashCode()
        {
            return BoundingRect.GetHashCode();
        }

        public static bool operator==(ElementProperties ep1, ElementProperties ep2)
        {
            return ep1?.Equals(ep2) ?? ep2 is null;
        }

        public static bool operator!=(ElementProperties ep1, ElementProperties ep2)
        {
            return !(ep1 == ep2);
        }
    }
}
