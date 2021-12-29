using System;

namespace Outlines.Inspection
{
    public class TaskViewHelper
    {
        public void HideWindowFromTaskView(IntPtr hwnd)
        {
            int windowStyle = NativeWindowService.GetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE);
            windowStyle |= (int)NativeWindowService.ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            NativeWindowService.SetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE, windowStyle);
        }
    }
}
