using System.Windows;

namespace Redlines
{
    public class ElementProperties
    {
        public Rect BoundingRect { get; set; }

        public override int GetHashCode()
        {
            return BoundingRect.GetHashCode();
        }
    }
}
