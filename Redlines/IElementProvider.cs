using System.Windows;
using System.Windows.Automation;

namespace Redlines
{
    public interface IElementProvider
    {
        AutomationElement TryGetElementFromPoint(Point point);
    }
}
