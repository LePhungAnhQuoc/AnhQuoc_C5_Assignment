using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class SelectedItemRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            object getValue = value;
            if (getValue == null)
            {
                return new ValidationResult(false, Utilities.ValidateNoteSelectedItemRule());
            }
            return ValidationResult.ValidResult;
        }
    }
}
