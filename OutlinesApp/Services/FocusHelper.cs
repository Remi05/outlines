using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace OutlinesApp.Services
{
    public class FocusHelper
    {
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga
        private const int GWL_EXSTYLE = -20;

        // Based on https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
        private const int WS_EX_NOACTIVATE = 0x08000000;

        public void DisableTakingFocus(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            int curWindowStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, curWindowStyle | WS_EX_NOACTIVATE);
        }
    }
}
