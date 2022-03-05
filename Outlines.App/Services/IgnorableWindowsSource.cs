using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Outlines.Inspection;

namespace Outlines.App.Services
{
    public class IgnorableWindowsSource : IIgnorableWindowsSource
    {
        private Dispatcher Dispatcher { get; set; }

        public IgnorableWindowsSource(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        public ISet<IntPtr> GetWindowsToIgnore()
        {
            var windowsToIgnore = new HashSet<IntPtr>();
            Dispatcher.Invoke(() =>
            {
                foreach (Window window in App.Current.Windows)
                {
                    windowsToIgnore.Add(new WindowInteropHelper(window).EnsureHandle());
                }
            });            
            return windowsToIgnore;
        }
    }
}
