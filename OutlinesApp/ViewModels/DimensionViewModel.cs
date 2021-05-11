using System.Windows;
using Outlines;

namespace OutlinesApp.ViewModels
{
    public class DimensionViewModel
    {
        private const int ContainerRectWidth = 100;
        private const int ContainerRectHeight = 50;

        private ElementProperties ElementProperties { get; set; }
        private IScreenHelper ScreenHelper { get; set; }

        public string DimensionsText => $"{ElementProperties.BoundingRect.Width}x{ElementProperties.BoundingRect.Height}";

        public Rect ContainerRect { get; private set; }

        public DimensionViewModel(ElementProperties elementProperties, IScreenHelper screenHelper)
        {
            ElementProperties = elementProperties;
            ScreenHelper = screenHelper;
            InitializeContainerRect();
        }

        private void InitializeContainerRect()
        {
            var localElementRect = ScreenHelper.RectFromScreen(ElementProperties.BoundingRect);
            var localElementBottomCenter = Point.Add(localElementRect.BottomLeft, new Vector(localElementRect.Width / 2, 0));
            var containerTopLeft = new Point(localElementBottomCenter.X - ContainerRectWidth / 2, localElementBottomCenter.Y);
            ContainerRect = new Rect(containerTopLeft, new Size(ContainerRectWidth, ContainerRectHeight));
        }
    }
}
