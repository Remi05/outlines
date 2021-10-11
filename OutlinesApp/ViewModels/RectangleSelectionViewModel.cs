using System.ComponentModel;
using System.Windows;

namespace OutlinesApp.ViewModels
{
    public delegate void RectangleSelectedHandler(Rect rectangle);

    public class RectangleSelectionViewModel : INotifyPropertyChanged
    {
        private bool IsDragging { get; set; }

        private Point InitialPosition { get; set; }

        private Rect relativeRectangleBounds = new Rect(0, 0, 0, 0);
        public Rect RelativeRectangleBounds
        {
            get => relativeRectangleBounds;
            private set
            {
                if (value != relativeRectangleBounds)
                {
                    relativeRectangleBounds = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RelativeRectangleBounds)));
                }
            }
        }

        public event RectangleSelectedHandler RectangleSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnMouseDown(Point relativePosition)
        {
            InitialPosition = relativePosition;
            RelativeRectangleBounds = new Rect(InitialPosition, new Size(0, 0));
            IsDragging = true;
        }

        public void OnMouseUp(Point relativePosition)
        {
            IsDragging = false;
            RectangleSelected?.Invoke(new Rect(InitialPosition, relativePosition));
            RelativeRectangleBounds = new Rect(0, 0, 0, 0);
        }

        public void OnMouseMove(Point relativePosition)
        {
            if (IsDragging)
            {
                RelativeRectangleBounds = new Rect(InitialPosition, relativePosition);
            }
        }
    }
}
