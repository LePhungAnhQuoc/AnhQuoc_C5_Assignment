using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public static class StringExtension
    {
        public static bool Contains(this string source, string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                value = value.ToLower();
                source = source.ToLower();
            }
            bool result = source.Contains(value);
            return result;
        }

        public static bool ContainsCorrectly(this string source, string value, bool ignoreCase, bool ignoringAccentedLetters = true)
        {
            if (ignoreCase)
            {
                value = value.ToLower();
                source = source.ToLower();
            }
            if (ignoringAccentedLetters)
            {
                value = Utilities.RemoveDiacritics(value);
                source = Utilities.RemoveDiacritics(source);
            }
            
            bool isContains = Contains(source, value, ignoreCase);
            if (isContains)
            {
                int indexOf = source.IndexOf(value, StringComparison.OrdinalIgnoreCase);
                if (indexOf == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
