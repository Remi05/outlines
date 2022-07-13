using System;

namespace Outlines.Inspection
{
    public class WindowZOrderHelper
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        public void MoveWindowToUIAccessZBand(IntPtr hwnd)
        {
            var flags = NativeWindowService.SetWindowPosFlags.SWP_NOSIZE 
                      | NativeWindowService.SetWindowPosFlags.SWP_NOMOVE 
                      | NativeWindowService.SetWindowPosFlags.SWP_NOACTIVATE;
            NativeWindowService.SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, flags);
        }

        public void ShowWindowNoZOrderChange(IntPtr hwnd)
        {
            var flags = NativeWindowService.SetWindowPosFlags.SWP_SHOWWINDOW
                      | NativeWindowService.SetWindowPosFlags.SWP_NOZORDER
                      | NativeWindowService.SetWindowPosFlags.SWP_NOSIZE
                      | NativeWindowService.SetWindowPosFlags.SWP_NOMOVE
                      | NativeWindowService.SetWindowPosFlags.SWP_NOACTIVATE;
            NativeWindowService.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, flags);
        }

        public void HideWindowNoZOrderChange(IntPtr hwnd)
        {
            var flags = NativeWindowService.SetWindowPosFlags.SWP_HIDEWINDOW
                      | NativeWindowService.SetWindowPosFlags.SWP_NOZORDER
                      | NativeWindowService.SetWindowPosFlags.SWP_NOSIZE
                      | NativeWindowService.SetWindowPosFlags.SWP_NOMOVE
                      | NativeWindowService.SetWindowPosFlags.SWP_NOACTIVATE;
            NativeWindowService.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, flags);
        }
    }
}
