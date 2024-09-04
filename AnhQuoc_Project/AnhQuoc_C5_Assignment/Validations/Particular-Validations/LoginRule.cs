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

            if (Utilitys.IsCheckEmptyString(strInput))
                return new ValidationResult(false, Utilitys.ValidateNoteFormNotEmptyRule());

            if (Utilitys.InputUnicodeTextNumberRule(strInput, true) == false)
                return new ValidationResult(false, Utilitys.ValidateNoteInputUnicodeTextNumberRule());

            return ValidationResult.ValidResult;
        }
    }
}
