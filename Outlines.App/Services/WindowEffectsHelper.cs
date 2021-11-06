using System;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace Outlines.App.Services
{
    public class WindowEffectsHelper
    {
        // Based on https://docs.microsoft.com/en-us/windows/win32/gdi/colorref
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
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_MICA_EFFECT = 1029,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref int attributeValue, uint attributeValueSize);

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref COLORREF attributeValue, uint attributeValueSize);

        public void SetTitlebarBackgroundColor(IntPtr hwnd, Color titlebarBackgroundColor)
        {
            var nativeColor = new COLORREF() { R = titlebarBackgroundColor.R, G = titlebarBackgroundColor.G, B = titlebarBackgroundColor.B };
            DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR, ref nativeColor, sizeof(uint));
        }

        public void SetShouldUseDarkMode(IntPtr hwnd, bool shouldUseDarkMode)
        {
            int shouldUseDarkModeValue = shouldUseDarkMode ? 0x0001 : 0x0000;
            DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref shouldUseDarkModeValue, sizeof(int));
        }

        public void SetMicaEffect(IntPtr hwnd, bool isMicaEnabled)
        {
            int isMicaEnabledValue = isMicaEnabled ? 0x0001 : 0x0000;
            DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT, ref isMicaEnabledValue, sizeof(int));
        }
    }
}
