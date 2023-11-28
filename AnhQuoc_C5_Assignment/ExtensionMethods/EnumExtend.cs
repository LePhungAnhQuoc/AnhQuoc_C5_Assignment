using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public static class EnumExtend
    {
        public static bool ConvertValue(this ReaderType value)
        {
            switch (value)
            {
                case ReaderType.Adult: return true;
                case ReaderType.Child: return false;
            }
            return false;
        }
    }
}
