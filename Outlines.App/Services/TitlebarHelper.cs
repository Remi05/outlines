using System;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace Outlines.App.Services
{
    public class TitlebarHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct COLORREF
        {
            public byte R;
            public byte G;
            public byte B;
        }

        private enum DWMWINDOWATTRIBUTE
        {
            DWMWA_CAPTION_COLOR = 35,
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref COLORREF pvAttribute, uint cbAttribute);

        public void SetTitlebarBackgroundColor(IntPtr hwnd, Color titlebarBackgroundColor)
        {
            var nativeColor = new COLORREF() { R = titlebarBackgroundColor.R, G = titlebarBackgroundColor.G, B = titlebarBackgroundColor.B };
            DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR, ref nativeColor, sizeof(uint));
        }
    }
}
