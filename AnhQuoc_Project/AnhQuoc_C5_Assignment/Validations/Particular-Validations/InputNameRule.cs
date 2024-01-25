using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputNameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;

            if (Utilities.IsCheckEmptyString(getValue))
            {
                return new ValidationResult(false, Utilities.ValidateNoteFormNotEmptyRule());
            }

            if (!Utilities.InputUnicodeTextRule(getValue, true))
            {
                return new ValidationResult(false, Utilities.ValidateNoteInputUnicodeTextRule());
            }
            return ValidationResult.ValidResult;
        }
    }
}
