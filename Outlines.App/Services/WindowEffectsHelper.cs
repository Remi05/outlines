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

        private enum DwmWindowAttributes
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_CAPTION_COLOR = 35,
            DWMWA_SYSTEMBACKDROP_TYPE = 38,
            DWMWA_MICA_EFFECT = 1029,
        }

        private enum SystemBackdropTypes
        {
            DWMSBT_AUTO = 0,
            DWMSBT_DISABLE = 1,
            DWMSBT_MAINWINDOW = 2,
            DWMSBT_TRANSIENTWINDOW = 3,
            DWMSBT_TABBEDWINDOW = 4,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributes attribute, ref int attributeValue, uint attributeValueSize);

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributes attribute, ref SystemBackdropTypes attributeValue, uint attributeValueSize);

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributes attribute, ref COLORREF attributeValue, uint attributeValueSize);

        public void SetTitlebarBackgroundColor(IntPtr hwnd, Color titlebarBackgroundColor)
        {
            var nativeColor = new COLORREF() { R = titlebarBackgroundColor.R, G = titlebarBackgroundColor.G, B = titlebarBackgroundColor.B };
            DwmSetWindowAttribute(hwnd, DwmWindowAttributes.DWMWA_CAPTION_COLOR, ref nativeColor, sizeof(uint));
        }

        public void SetShouldUseDarkMode(IntPtr hwnd, bool shouldUseDarkMode)
        {
            int shouldUseDarkModeValue = shouldUseDarkMode ? 0x0001 : 0x0000;
            DwmSetWindowAttribute(hwnd, DwmWindowAttributes.DWMWA_USE_IMMERSIVE_DARK_MODE, ref shouldUseDarkModeValue, sizeof(int));
        }

        public void SetMicaEffect(IntPtr hwnd, bool isMicaEnabled)
        {
            if (Environment.OSVersion.Version.Build >= 22523)
            {
                // The DWMSBT_MAINWINDOW backdrop type (Mica) was introduced in build 22523, there currently is no API contract we could check for instead.
                SystemBackdropTypes backropType = isMicaEnabled ? SystemBackdropTypes.DWMSBT_MAINWINDOW : SystemBackdropTypes.DWMSBT_AUTO;
                DwmSetWindowAttribute(hwnd, DwmWindowAttributes.DWMWA_SYSTEMBACKDROP_TYPE, ref backropType, sizeof(int));
            }
            else
            {
                // DWMWA_MICA_EFFECT is an undocumented window attribute that worked in early Windows 11 builds.
                int isMicaEnabledValue = isMicaEnabled ? 0x0001 : 0x0000;
                DwmSetWindowAttribute(hwnd, DwmWindowAttributes.DWMWA_MICA_EFFECT, ref isMicaEnabledValue, sizeof(int));
            }
        }
    }
}
