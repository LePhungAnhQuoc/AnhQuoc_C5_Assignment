using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputNumberRule : ValidationRule
    {
        public InputNumberRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;
            if (Utilitys.FormNotEmptyRule(value))
            {
                if (!Utilitys.InputNumberRule(getValue))
                {
                    return new ValidationResult(false, Utilitys.ValidateNoteInputNumberRule());
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
