using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

namespace OutlinesApp
{
    public class ColorPickerService : IColorPickerService
    {
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hDC, int xPos, int yPos);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        public Color GetColorAt(Point point)
        {
            IntPtr windowDC = GetWindowDC(IntPtr.Zero);
            uint color = GetPixel(windowDC, (int)point.X, (int)point.Y);
            byte[] colorBytes = BitConverter.GetBytes(color);
            return Color.FromArgb(colorBytes[3], colorBytes[0], colorBytes[1], colorBytes[2]);
        }
    }
}
