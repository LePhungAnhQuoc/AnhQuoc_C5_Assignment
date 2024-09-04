using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public static class ControlExtend
    {
        public static List<T> GetLogicalChildCollection<T>(this DependencyObject parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            Utilitys.GetLogicalChildCollection(parent, logicalCollection);
            return logicalCollection;
        }
    }
}
