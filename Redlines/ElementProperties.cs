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
    }
}
