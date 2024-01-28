using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public static class IEnumerableExtendExtend
    {
        public static TColl ToTypedCollection<TColl, T>(this IEnumerable ien)
        where TColl : IList<T>, new()
        {
            TColl collection = new TColl();

            foreach (var item in ien)
            {
                collection.Add((T)item);
            }

            return collection;
        }

        public static List<object> ToListObject(this IEnumerable ien)
        {
            List<object> collection = new List<object>();

            foreach (var item in ien)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
