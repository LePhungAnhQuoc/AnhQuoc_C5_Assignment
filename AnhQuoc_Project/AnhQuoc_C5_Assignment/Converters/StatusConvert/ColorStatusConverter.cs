using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AnhQuoc_C5_Assignment
{
    public class ColorStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool getValue = (bool)value;
            SolidColorBrush result = new SolidColorBrush(Colors.Black);

            if (getValue == true)
                result.Color = Colors.LightGreen;
            else
                result.Color = Colors.Red;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
