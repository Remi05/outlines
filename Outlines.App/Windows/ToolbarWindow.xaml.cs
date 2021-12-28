using System.Windows;
using System.Windows.Input;

namespace Outlines.App
{
    public partial class ToolbarWindow : Window
    {
        public ToolbarWindow()
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
