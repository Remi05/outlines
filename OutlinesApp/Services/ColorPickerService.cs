using System;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace Outlines.App.Services
{
    public class ColorPickerService : IColorPickerService
    {
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hDC, int xPos, int yPos);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        public Color GetColorAt(System.Drawing.Point point)
        {
            IntPtr windowDC = GetWindowDC(IntPtr.Zero);
            uint color = GetPixel(windowDC, point.X, point.Y);
            byte[] colorBytes = BitConverter.GetBytes(color);
            return Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
        }
    }
}
