using System;

namespace Outlines.Inspection
{
    public class UiaWindowHelper
    {
        public void HideWindowFromUia(IntPtr hwnd)
        {
            int windowStyle = NativeWindowService.GetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE);
            windowStyle |= (int)(NativeWindowService.ExtendedWindowStyles.WS_EX_LAYERED | NativeWindowService.ExtendedWindowStyles.WS_EX_TRANSPARENT);
            NativeWindowService.SetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE, windowStyle);
        }
    }
}
