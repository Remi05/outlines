using System;
using System.Collections.Generic;

namespace Outlines.Inspection
{
    public interface IIgnorableWindowsSource
    {
        ISet<IntPtr> GetWindowsToIgnore();
    }
}