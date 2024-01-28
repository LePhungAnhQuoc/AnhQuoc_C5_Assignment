using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime getValue = (DateTime)value;

            if (!Utilities.DateTimeRangeRule(getValue, Constants.dateMinValue, Constants.dateMaxValue()))
            {
                return new ValidationResult(false, Utilities.ValidateNoteDateTimeRangeRule(Constants.dateMinValue));
            }
            return ValidationResult.ValidResult;
        }
    }
}
