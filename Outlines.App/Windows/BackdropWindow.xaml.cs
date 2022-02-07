using System;
using System.Windows;
using System.Windows.Interop;
using Outlines.Inspection;

namespace Outlines.App
{
    public partial class BackdropWindow : Window
    {
        public BackdropWindow()
        {
            InitializeComponent();

            IntPtr hwnd = new WindowInteropHelper(this).EnsureHandle();

            var focusHelper = new FocusHelper();
            focusHelper.DisableTakingFocus(hwnd);

            var taskViewHelper = new TaskViewHelper();
            taskViewHelper.HideWindowFromTaskView(hwnd);

            var windowZOrderHelper = new WindowZOrderHelper();
            windowZOrderHelper.MoveWindowToUIAccessZBand(hwnd);
        }
    }
}
