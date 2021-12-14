using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FitSystem.ValueConverters
{
    public class TextToDecimalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((decimal)value == 0.0m)
            {
                return null;
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                decimal newDecimal = System.Convert.ToDecimal(value);
                return newDecimal;
            }
            catch { }
            return 0.0m;
        }
    }
}
