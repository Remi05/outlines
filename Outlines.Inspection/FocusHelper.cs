using System;

namespace Outlines.Inspection
{
    public class FocusHelper
    {
        public void DisableTakingFocus(IntPtr hwnd)
        {
            int curWindowStyle = NativeWindowService.GetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE);
            int newWindowStyle = curWindowStyle | (int)NativeWindowService.ExtendedWindowStyles.WS_EX_NOACTIVATE;
            NativeWindowService.SetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE, newWindowStyle);
        }
    }
}
