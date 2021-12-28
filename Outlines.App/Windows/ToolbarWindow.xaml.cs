using System.Windows;
using System.Windows.Input;

namespace Outlines.App
{
    public partial class ToolBarWindow : Window
    {
        public ToolBarWindow()
        {
            InitializeComponent();
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
