using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace OutlinesApp
{
    public static class ScreenHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool shouldRepaint);

        public static void CoverAllDisplays(IntPtr hWnd)
        {
            MoveWindow(hWnd, (int)SystemParameters.VirtualScreenLeft, (int)SystemParameters.VirtualScreenTop, (int)SystemParameters.VirtualScreenWidth, (int)SystemParameters.VirtualScreenHeight, true);
        }
    }
}
