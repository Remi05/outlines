using System;

namespace Outlines.Inspection
{
    public class FocusHelper
    {
        public void DisableTakingFocus(IntPtr hwnd)
        {
            int curWindowStyle = NativeWindowService.GetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE);
            NativeWindowService.SetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE, curWindowStyle | (int)NativeWindowService.ExtendedWindowStyles.WS_EX_NOACTIVATE);
        }
    }
}
