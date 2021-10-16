using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Outlines.Inspection
{
    public class WindowProvider : IWindowProvider
    {
        private enum ChildWindowFromPointFlags : uint
        {
            CWP_ALL = 0x0000,
            CWP_SKIPINVISIBLE = 0x0001,
            CWP_SKIPDISABLED = 0x0002,
            CWP_SKIPTRANSPARENT = 0x0004
        }

        // Based on https://docs.microsoft.com/en-us/previous-versions/dd162805(v=vs.85)
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        static extern IntPtr ChildWindowFromPointEx(IntPtr hwnd, POINT point, uint flags);

        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();

        public IntPtr TryGetWindowFromPoint(Point point)
        {
            IntPtr rootWindow = GetDesktopWindow();
            POINT nativePoint = new POINT() { x = point.X, y = point.Y };
            var flags = ChildWindowFromPointFlags.CWP_SKIPDISABLED
                      | ChildWindowFromPointFlags.CWP_SKIPINVISIBLE
                      | ChildWindowFromPointFlags.CWP_SKIPTRANSPARENT;
            return ChildWindowFromPointEx(rootWindow, nativePoint, (uint)flags);
        }
    }
}

