using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputIdentifyRule : ValidationRule
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

            int fixLength = Constants.textIdentifyLength;
            if (!Utilities.FixLengthRule(getValue, fixLength))
            {
                return new ValidationResult(false, Utilities.ValidateNoteFixLengthRule(fixLength));
            }
            return ValidationResult.ValidResult;
        }
    }
}
