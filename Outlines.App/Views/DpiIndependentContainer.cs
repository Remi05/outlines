using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Outlines.App.Views
{
    public class DpiIndependentContainer : Decorator
    {
        public DpiIndependentContainer()
        {
            Loaded += (_, __) => OnLoaded();
        }

        private void OnLoaded()
        {
            Matrix transformToDevice = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice;
            var inverseDpiTransform = new ScaleTransform(1.0 / transformToDevice.M11, 1.0 / transformToDevice.M22);
            if (inverseDpiTransform.CanFreeze)
            {
                inverseDpiTransform.Freeze();
            }
            LayoutTransform = inverseDpiTransform;
        }
    }
}
