using System.Drawing;

namespace Outlines.Core
{
    public interface IScreenHelper
    {
        Rectangle GetDisplayRect(Point point);
        double GetDisplayScaleFactor();
    }
}