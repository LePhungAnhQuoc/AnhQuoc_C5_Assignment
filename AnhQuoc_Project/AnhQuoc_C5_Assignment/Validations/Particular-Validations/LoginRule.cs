using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{ 
    public class LoginRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strInput = (string)value;

            if (Utilities.IsCheckEmptyString(strInput))
                return new ValidationResult(false, Utilities.ValidateNoteFormNotEmptyRule());

            if (Utilities.InputUnicodeTextNumberRule(strInput, true) == false)
                return new ValidationResult(false, Utilities.ValidateNoteInputUnicodeTextNumberRule());

            return ValidationResult.ValidResult;
        }
    }
}
