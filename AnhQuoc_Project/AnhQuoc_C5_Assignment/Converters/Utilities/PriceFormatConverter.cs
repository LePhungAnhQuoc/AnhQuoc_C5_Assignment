using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    public class PriceFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            if (Utilities.IsCheckEmptyString(value.ToString()))
                return string.Empty;

            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            value = Utilities.FormatCurrency("VND", System.Convert.ToDecimal(value.ToString()));

            value = value.ToString().RemoveString(" VND") + ".000" + " VND";

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
