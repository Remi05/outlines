using System;

namespace Outlines.Inspection
{
    public class UiaWindowHelper
    {
        public void HideWindowFromUia(IntPtr hwnd)
        {
            //const string windowVisibilityPropertyName = "UIA_WindowVisibilityOverriden";
            //const int hideWindowPropertyValue = 2;
            //bool success = NativeWindowService.SetProp(hwnd, windowVisibilityPropertyName, new IntPtr(hideWindowPropertyValue));
            //IntPtr val = NativeWindowService.GetProp(hwnd, windowVisibilityPropertyName);


            //uint isCloakedValue = 1;
            //NativeWindowService.DwmSetWindowAttribute(hwnd, NativeWindowService.DwmWindowAttributes.DWMWA_CLOAKED, isCloakedValue, sizeof(uint));

            int curWindowStyle = NativeWindowService.GetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE);
            NativeWindowService.SetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE, curWindowStyle | (int)(NativeWindowService.ExtendedWindowStyles.WS_EX_LAYERED | NativeWindowService.ExtendedWindowStyles.WS_EX_TRANSPARENT));
        }
    }
}
