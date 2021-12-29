using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Outlines.Inspection;

namespace Outlines.App
{
    public partial class TreeViewWindow : Window
    {
        public TreeViewWindow()
        {
            InitializeComponent();

            IntPtr hwnd = new WindowInteropHelper(this).EnsureHandle();

            var focusHelper = new FocusHelper();
            focusHelper.DisableTakingFocus(hwnd);

            var taskViewHelper = new TaskViewHelper();
            taskViewHelper.HideWindowFromTaskView(hwnd);
        }

        private void OnHandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
