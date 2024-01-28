using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public static class ObservableCollectionExtend
    {
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                source.Add(item);
            }
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> result = new ObservableCollection<T>(source);
            return result;
        }




        public static ObservableCollection<object> ToObservableCollectionObj<T>(this IEnumerable<T> source)
        {
            ObservableCollection<object> result = new ObservableCollection<object>();
            foreach (var item in source)
            {
                result.Add(item);
            }
            return result;
        }

        public static ObservableCollection<T> Clone<T>(this ObservableCollection<T> source) where T: class, ICloneable
        {
            var result = source.Select((item) =>
            {
                return item.Clone() as T;
            }).ToObservableCollection();
            return result;
        }
    }
}
