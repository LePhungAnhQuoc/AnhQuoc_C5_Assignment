using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputQuantityRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;

            if (Utilities.IsCheckEmptyString(getValue))
            {
                return new ValidationResult(false, Utilities.ValidateNoteFormNotEmptyRule());
            }

            if (!Utilities.InputNumberRule(getValue))
            {
                return new ValidationResult(false, Utilities.ValidateNoteInputNumberRule());
            }

            int getValueNumber = Convert.ToInt32(getValue);
            if (getValueNumber == 0)
            {
                return new ValidationResult(false, Utilities.ValidateNoteQuantityEqual0());
            }

            return ValidationResult.ValidResult;
        }
    }
}
