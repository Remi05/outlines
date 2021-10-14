using System;
using System.ComponentModel;
using System.Windows.Media;
using Outlines.Inspection.Common;
using OutlinesApp.Services;

namespace OutlinesApp.ViewModels
{
    public class ColorPickerViewModel : INotifyPropertyChanged
    {
        private const int HoverDelayInMs = 50;

        private IColorPickerService ColorPickerService { get; set; }
        private IGlobalInputListener GlobalInputListener { get; set; }

        private Color pickedColor = Colors.Black;
        public Color PickedColor
        {
            get => pickedColor;
            private set
            {
                if (value != pickedColor)
                {
                    pickedColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PickedColor)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PickedColorBrush)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PickedColorRbg)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PickedColorHex)));
                }
            }
        }

        public Brush PickedColorBrush => new SolidColorBrush(PickedColor);

        public string PickedColorRbg => PickedColor == null ? "" : $"({PickedColor.R},{PickedColor.G},{PickedColor.B})";

        public string PickedColorHex => PickedColor == null ? "" : $"#{PickedColor.R.ToString("X2")}{PickedColor.G.ToString("X2")}{PickedColor.B.ToString("X2")}";

        public event PropertyChangedEventHandler PropertyChanged;

        public ColorPickerViewModel(IColorPickerService colorPickerService, IGlobalInputListener globalInputListener)
        {
            if (colorPickerService == null || globalInputListener == null)
            {
                throw new ArgumentNullException(colorPickerService == null ? nameof(colorPickerService) : nameof(globalInputListener));
            }
            ColorPickerService = colorPickerService;
            GlobalInputListener = globalInputListener;

            var hoverWatcher = new HoverWatcher(HoverDelayInMs);
            hoverWatcher.MouseHovered += OnMouseHovered;
            GlobalInputListener.MouseMoved += hoverWatcher.OnMouseMoved;
        }

        private void OnMouseHovered(System.Drawing.Point cursorPos)
        {
            PickedColor = ColorPickerService.GetColorAt(cursorPos);
        }
    }
}
