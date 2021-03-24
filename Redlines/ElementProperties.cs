using System.Windows;

namespace Redlines
{
    public class ElementProperties
    {
        public Rect BoundingRect { get; set; }

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
