using System;
using System.Drawing;

namespace Outlines.Inspection
{
    public interface IWindowProvider
    {
        IntPtr TryGetWindowFromPoint(Point point);
    }
}
