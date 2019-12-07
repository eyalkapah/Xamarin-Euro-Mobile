using System;
using System.Globalization;
using Xamarin.Forms;

namespace EuroMobile.Converters
{
    public class InverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var val = bool.TryParse(value.ToString(), out bool result);

            if (val)
                return !result;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}