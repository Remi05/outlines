using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Outlines.Inspection;

namespace Outlines.App
{
    public partial class PropertiesWindow : Window
    {
        public IntPtr Hwnd { get; private set; }

        public PropertiesWindow()
        {
            InitializeComponent();

            Hwnd = new WindowInteropHelper(this).EnsureHandle();

            var focusHelper = new FocusHelper();
            focusHelper.DisableTakingFocus(Hwnd);

            var taskViewHelper = new TaskViewHelper();
            taskViewHelper.HideWindowFromTaskView(Hwnd);

            var windowZOrderHelper = new WindowZOrderHelper();
            windowZOrderHelper.MoveWindowToUIAccessZBand(Hwnd);
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
