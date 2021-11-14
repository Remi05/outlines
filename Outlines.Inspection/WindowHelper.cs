using System;
using System.Drawing;

namespace Outlines.Inspection
{
    public class WindowHelper
    {
        public void CoverAllDisplays(IntPtr hwnd)
        {
            int virtualScreenX = NativeWindowService.GetSystemMetrics(NativeWindowService.SystemMetricsIndices.SM_XVIRTUALSCREEN);
            int virtualScreenY = NativeWindowService.GetSystemMetrics(NativeWindowService.SystemMetricsIndices.SM_YVIRTUALSCREEN);
            int virtualScreenWidth = NativeWindowService.GetSystemMetrics(NativeWindowService.SystemMetricsIndices.SM_CXVIRTUALSCREEN);
            int virtualScreenHeight = NativeWindowService.GetSystemMetrics(NativeWindowService.SystemMetricsIndices.SM_CYVIRTUALSCREEN);
            Rectangle virtualScreenRect = new Rectangle(virtualScreenX, virtualScreenY, virtualScreenWidth, virtualScreenHeight);
            NativeWindowService.MoveWindow(hwnd, virtualScreenRect.X, virtualScreenRect.Y, virtualScreenRect.Width, virtualScreenRect.Height, true);
        }
    }
}
