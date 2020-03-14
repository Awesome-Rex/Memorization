using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Practice_Dojang.Scripts.ViewModels.Converters
{
    public class ConditionalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool condition = (bool)value;

            string[] parameters = ((string)parameter).Split('~');

            if (condition)
            {
                return (object)parameters[0];
            } else
            {
                return (object)parameters[1];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
