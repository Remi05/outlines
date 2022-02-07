using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using Outlines.Inspection;

namespace Outlines.App.Services
{
    public class IgnorableWindowsSource : IIgnorableWindowsSource
    {
        protected ISet<IntPtr> WindowsToIgnore { get; set; } = new HashSet<IntPtr>();

        public ISet<IntPtr> GetWindowsToIgnore() => WindowsToIgnore;

        public void IgnoreWindow(Window window)
        {
            if (window != null)
            {
                IntPtr hwnd = new WindowInteropHelper(window).Handle;
                WindowsToIgnore.Add(hwnd);
            }
        }
    }
}
