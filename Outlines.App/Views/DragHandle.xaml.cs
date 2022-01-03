using System.Windows;
using System.Windows.Controls;

namespace Outlines.App.Views
{
    public partial class DragHandle : UserControl
    {
        public static readonly DependencyProperty OrientationProperty = 
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DragHandle), new PropertyMetadata(Orientation.Horizontal));

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public DragHandle()
        {
            InitializeComponent();
        }
    }
}
