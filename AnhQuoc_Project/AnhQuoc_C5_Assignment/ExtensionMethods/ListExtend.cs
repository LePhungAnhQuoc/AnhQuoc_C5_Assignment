using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public static class ListExtend
    {
        public static List<T> Clone<T>(this List<T> source) where T: class, ICloneable
        {
            var result = source.Select((item) =>
            {
                return item.Clone() as T;
            }).ToList();
            return result;
        }
    }
}
