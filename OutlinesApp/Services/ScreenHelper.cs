using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;
using Outlines.Core;

namespace OutlinesApp.Services
{
    public class ScreenHelper : IScreenHelper
    {
        private Visual RootVisual { get; set; }

        public ScreenHelper(Visual rootVisual)
        {
            RootVisual = rootVisual;
        }

        public Rectangle GetDisplayRect(Point point)
        {
            return Screen.FromPoint(point).Bounds;
        }

        public double GetDisplayScaleFactor()
        {
            return System.Windows.PresentationSource.FromVisual(RootVisual).CompositionTarget.TransformToDevice.M11;
        }
    }
}
