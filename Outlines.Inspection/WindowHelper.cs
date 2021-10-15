using System;
using System.Drawing;
using System.Runtime.InteropServices;
//using System.Windows.Forms;

namespace Outlines.Inspection
{
    public class WindowHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool shouldRepaint);

        public void CoverAllDisplays(IntPtr hWnd)
        {
            Rectangle displaysRect = Rectangle.Empty; // SystemInformation.VirtualScreen;
            MoveWindow(hWnd, displaysRect.X, displaysRect.Y, displaysRect.Width, displaysRect.Height, true);
        }
    }
}
