using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LoadingSpinnerControl.Converters
{
    public class DiameterAndStrokeThicknessToStrokeDashArrayConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double diameter, strokeThickness;
            if (values.Length < 2
                || !double.TryParse(values[0].ToString(), out diameter)
                || !double.TryParse(values[1].ToString(), out strokeThickness))
            {
                return 0;
            }

            double circumPerfence = Math.PI * diameter;
            double lineLength = circumPerfence * 0.75;
            double gapLength = circumPerfence - lineLength;

            return new DoubleCollection(new[] { lineLength / strokeThickness, gapLength / strokeThickness });
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
