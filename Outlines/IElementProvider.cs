using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public interface IElementProvider
    {
        AutomationElement TryGetElementFromPoint(Point point);
    }
}
