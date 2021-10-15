using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Outlines.App
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object param, CultureInfo cultureInfo)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object param, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
