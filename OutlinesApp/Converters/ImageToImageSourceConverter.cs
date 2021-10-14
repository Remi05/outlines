using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OutlinesApp
{
    public class ImageToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object param, CultureInfo cultureInfo)
        {
            var memoryStream = new MemoryStream();  
            (value as Image)?.Save(memoryStream, ImageFormat.Png);

            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = memoryStream;
            imageSource.EndInit();        
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object param, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
