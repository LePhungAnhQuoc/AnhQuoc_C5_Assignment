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

            if (Utilitys.IsCheckEmptyString(getValue))
            {
                return new ValidationResult(false, Utilitys.ValidateNoteFormNotEmptyRule());
            }

            if (!Utilitys.InputNumberRule(getValue))
            {
                return new ValidationResult(false, Utilitys.ValidateNoteInputNumberRule());
            }

            int fixLength = Constants.textIdentifyLength;
            if (!Utilitys.FixLengthRule(getValue, fixLength))
            {
                return new ValidationResult(false, Utilitys.ValidateNoteFixLengthRule(fixLength));
            }
            return ValidationResult.ValidResult;
        }
    }
}
