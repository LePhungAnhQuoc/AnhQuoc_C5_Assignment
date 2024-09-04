using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputUnicodeTextNumberRule : ValidationRule
    {
        public bool IsAllowSpace { get; set; }

        public InputUnicodeTextNumberRule()
        {
            IsAllowSpace = true;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;
            if (Utilitys.FormNotEmptyRule(value))
            {
                if (!Utilitys.InputUnicodeTextNumberRule(getValue, IsAllowSpace))
                {
                    return new ValidationResult(false, Utilitys.ValidateNoteInputUnicodeTextNumberRule());
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
