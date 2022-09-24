using System;
using System.ComponentModel;
using System.Windows.Media;
using Outlines.App.Services;

namespace Outlines.App.ViewModels
{
    public class ColorPickerViewModel : INotifyPropertyChanged
    {
        private IColorPickerService ColorPickerService { get; set; }

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

        public string PickedColorRbg => $"({PickedColor.R},{PickedColor.G},{PickedColor.B})";

        public string PickedColorHex => $"#{PickedColor.R.ToString("X2")}{PickedColor.G.ToString("X2")}{PickedColor.B.ToString("X2")}";

        public event PropertyChangedEventHandler PropertyChanged;

        public ColorPickerViewModel(IColorPickerService colorPickerService)
        {
            if (colorPickerService == null)
            {
                throw new ArgumentNullException(nameof(colorPickerService));
            }
            ColorPickerService = colorPickerService;
        }

        public void OnMouseMoved(System.Drawing.Point cursorPos)
        {
            PickedColor = ColorPickerService.GetColorAt(cursorPos);
        }
    }
}
