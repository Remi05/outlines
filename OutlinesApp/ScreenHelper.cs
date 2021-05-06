using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OutlinesApp
{
    public static class ScreenHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool shouldRepaint);

        public static void CoverAllDisplays(Form form)
        {
            Rectangle displaysRect = SystemInformation.VirtualScreen;
            MoveWindow(form.Handle, displaysRect.X, displaysRect.Y, displaysRect.Width, displaysRect.Height, true);
        }
    }
}
