using System.Drawing;

namespace Outlines.Inspection
{
    public interface IScreenHelper
    {
        Rectangle GetDisplayRect(Point point);
        double GetDisplayScaleFactor();
    }
}