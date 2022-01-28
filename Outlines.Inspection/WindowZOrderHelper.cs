using System;

namespace Outlines.Inspection
{
    public class WindowZOrderHelper
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        public void MoveWindowToUIAccessZBand(IntPtr hwnd)
        {
            var flags = NativeWindowService.SetWindowPosFlags.SWP_NOSIZE | NativeWindowService.SetWindowPosFlags.SWP_NOMOVE | NativeWindowService.SetWindowPosFlags.SWP_NOACTIVATE;
            NativeWindowService.SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, flags);
        }
    }
}
