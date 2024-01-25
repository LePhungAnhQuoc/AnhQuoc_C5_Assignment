using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputNumberLengthRangeRule : ValidationRule
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        public InputNumberLengthRangeRule()
        {
            Min = null;
            Max = null;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Utilities.FormNotEmptyRule(value))
            {
                string getValue = (string)value;
                if (Utilities.InputNumberRule(getValue))
                {
                    int getLength = getValue.Length;

                    if (Utilities.IsCheckRange(getLength, Min, Max))
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, Utilities.ValidateNoteInputNumberLengthRangeRule(Min, Max));
                    }
                }
                else
                {
                    return new ValidationResult(false, Utilities.ValidateNoteInputNumberRule());
                }
            }
            else
            {
                return new ValidationResult(false, Utilities.ValidateNoteFormNotEmptyRule());
            }
        }
    }
}
