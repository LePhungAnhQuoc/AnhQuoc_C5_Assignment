using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputServerNameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;
            if (value == null)
            {
                return ValidationResult.ValidResult;
            }

            if (Utilitys.IsCheckEmptyString(getValue))
            {
                return new ValidationResult(false, Utilitys.ValidateNoteFormNotEmptyRule());
            }
            return ValidationResult.ValidResult;
        }
    }
}
