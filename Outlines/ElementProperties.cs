using System.Windows;

namespace Outlines
{
    public class ElementProperties
    {
        public string Name { get; private set; }
        public Rect BoundingRect { get; private set; }

        public ElementProperties(string name, Rect boundingRect)
        {
            Name = name;
            BoundingRect = boundingRect;
        }

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
