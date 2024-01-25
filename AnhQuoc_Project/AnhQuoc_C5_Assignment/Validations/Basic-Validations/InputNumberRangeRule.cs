using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputNumberRangeRule : ValidationRule
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        public InputNumberRangeRule()
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
                    int getNumberValue = Convert.ToInt32(getValue);
                    
                    if (Utilities.IsCheckRange(getNumberValue, Min, Max))
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, Utilities.ValidateNoteInputNumberRangeRule(Min, Max));
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
