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

            if (Utilitys.IsCheckEmptyString(getValue))
            {
                return new ValidationResult(false, Utilitys.ValidateNoteFormNotEmptyRule());
            }

            if (!Utilitys.InputUnicodeTextRule(getValue, true))
            {
                return new ValidationResult(false, Utilitys.ValidateNoteInputUnicodeTextRule());
            }
            return ValidationResult.ValidResult;
        }
    }
}
