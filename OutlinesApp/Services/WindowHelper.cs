using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OutlinesApp.Services
{
    public class WindowHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool shouldRepaint);

        public void CoverAllDisplays(IntPtr hWnd)
        {
            System.Drawing.Rectangle displaysRect = SystemInformation.VirtualScreen;
            MoveWindow(hWnd, displaysRect.X, displaysRect.Y, displaysRect.Width, displaysRect.Height, true);
        }
    }
}
