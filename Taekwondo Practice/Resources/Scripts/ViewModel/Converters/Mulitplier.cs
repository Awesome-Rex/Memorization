using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Taekwondo_Practice.Resources.Scripts.ViewModel.Converters
{
    public class Multiplier : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(double) && parameter.GetType() == typeof(double))
            {
                return (double)value * (double)parameter;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
