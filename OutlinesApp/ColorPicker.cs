using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OutlinesApp
{
    public class ColorPicker
    {
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hDC, int xPos, int yPos);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        public Color GetColorAt(Point point)
        {
            IntPtr windowDC = GetWindowDC(IntPtr.Zero);
            uint color = GetPixel(windowDC, point.X, point.Y);
            Color bgrColor = Color.FromArgb((int)color);
            return Color.FromArgb(bgrColor.B, bgrColor.G, bgrColor.R);
        }
    }
}
