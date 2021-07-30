using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public interface IElementProvider
    {
        ElementProperties TryGetElementFromPoint(Point point);
    }
}
