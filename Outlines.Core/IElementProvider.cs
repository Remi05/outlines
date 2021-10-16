using System;
using System.Drawing;

namespace Outlines.Core
{
    public interface IElementProvider
    {
        ElementProperties TryGetElementFromPoint(Point point);
        ElementProperties TryGetElementFromHandle(IntPtr handle);
    }
}
