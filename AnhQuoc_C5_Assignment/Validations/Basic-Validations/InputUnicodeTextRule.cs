using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputUnicodeTextRule : ValidationRule
    {
        public bool IsAllowSpace { get; set; }

        public InputUnicodeTextRule()
        {
            IsAllowSpace = true;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;
            if (Utilities.FormNotEmptyRule(value))
            {
                if (!Utilities.InputUnicodeTextRule(getValue, IsAllowSpace))
                {
                    return new ValidationResult(false, Utilities.ValidateNoteInputUnicodeTextRule());
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
