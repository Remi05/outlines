using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Outlines.Inspection;

namespace Outlines.App.Services
{
    public class ScreenHelper : IScreenHelper
    {
        private Window ReferenceWindow { get; set; }

        public ScreenHelper(Window referenceWindow)
        {
            ReferenceWindow = referenceWindow;
        }

        public Rectangle GetPrimaryDisplayRect()
        {
            return Screen.PrimaryScreen.Bounds;
        }

        public Rectangle GetDisplayRect(System.Drawing.Point point)
        {
            return Screen.GetBounds(point);
        }

        public double GetDisplayScaleFactor()
        {
            IntPtr hwnd = new WindowInteropHelper(ReferenceWindow).EnsureHandle();
            return NativeWindowService.GetDpiForWindow(hwnd) / 96.0; // A scale factor of 100% has a DPI of 96.
        }
    }
}
