using System;
using System.Windows;

namespace Outlines
{
    public interface INativeWindowService
    {
        IntPtr GetWindowFromPoint(Point point);
    }
}