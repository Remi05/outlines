using System.Drawing;

namespace Outlines.Inspection
{
    public interface IScreenHelper
    {
        Rectangle GetPrimaryDisplayRect();
        Rectangle GetDisplayRect(Point point);
        double GetDisplayScaleFactor();
    }
}