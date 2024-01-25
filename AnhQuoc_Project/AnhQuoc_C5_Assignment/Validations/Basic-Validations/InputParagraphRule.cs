using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class InputParagraphRule : ValidationRule
    {
        public bool IsAllowSpace { get; set; }
        public List<char> AllowCharacters { get; set; }

        public InputParagraphRule()
        {
            IsAllowSpace = true;
            AllowCharacters = Constants.allowCharacterInText;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string getValue = (string)value;
            if (Utilities.FormNotEmptyRule(value))
            {
                if (!Utilities.InputParagraphRule(getValue, IsAllowSpace, AllowCharacters))
                {
                    return new ValidationResult(false, Utilities.ValidateNoteInputParagraphRule());
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
