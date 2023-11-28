using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public static class BoolExtend
    {
        public static ReaderType ConvertValue(this bool value)
        {
            switch (value)
            {
                case true: return ReaderType.Adult;
                case false: return ReaderType.Child;
            }
            return ReaderType.Child;
        }
    }
}
