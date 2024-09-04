using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class FixLengthRule : ValidationRule
    {
        public int FixLength { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;
            if (Utilitys.FormNotEmptyRule(value))
            {
                if (Utilitys.InputNumberRule(getValue))
                {
                    int getLength = getValue.Length;

                    if (getLength == FixLength)
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, Utilitys.ValidateNoteFixLengthRule(FixLength));
                    }
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
