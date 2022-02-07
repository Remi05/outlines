using System;

namespace Outlines.Inspection
{
    public class FocusHelper
    {
        public void DisableTakingFocus(IntPtr hWnd)
        {
            int curWindowStyle = NativeWindowService.GetWindowLong(hWnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE);
            int newWindowStyle = curWindowStyle | (int)NativeWindowService.ExtendedWindowStyles.WS_EX_NOACTIVATE;
            NativeWindowService.SetWindowLong(hWnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE, newWindowStyle);
        }
    }
}
