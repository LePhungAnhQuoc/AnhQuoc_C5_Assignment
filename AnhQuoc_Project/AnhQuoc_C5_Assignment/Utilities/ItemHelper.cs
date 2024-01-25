using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class ItemHelper : DependencyObject
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached("IsChecked", typeof(bool?), typeof(ItemHelper), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCheckedPropertyChanged)));

        private static void OnIsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FunctionDto && ((bool?)e.NewValue).HasValue)
                foreach (FunctionChildDto p in (d as FunctionDto).Childs)
                    ItemHelper.SetIsChecked(p, (bool?)e.NewValue);

            if (d is FunctionChildDto)
            {
                int checking = ((d as FunctionChildDto).GetValue(ItemHelper.ParentProperty) as FunctionDto).Childs.Where(x => ItemHelper.GetIsChecked(x) == true).Count();
                int unchecking = ((d as FunctionChildDto).GetValue(ItemHelper.ParentProperty) as FunctionDto).Childs.Where(x => ItemHelper.GetIsChecked(x) == false).Count();
                if (unchecking > 0 && checking > 0)
                {
                    ItemHelper.SetIsChecked((d as FunctionChildDto).GetValue(ItemHelper.ParentProperty) as DependencyObject, null);
                    return;
                }
                if (checking > 0)
                {
                    ItemHelper.SetIsChecked((d as FunctionChildDto).GetValue(ItemHelper.ParentProperty) as DependencyObject, true);
                    return;
                }
                ItemHelper.SetIsChecked((d as FunctionChildDto).GetValue(ItemHelper.ParentProperty) as DependencyObject, false);
            }
        }

        public static void SetIsChecked(DependencyObject element, bool? IsChecked)
        {
            element.SetValue(ItemHelper.IsCheckedProperty, IsChecked);
        }

        public static bool? GetIsChecked(DependencyObject element)
        {
            return (bool?)element.GetValue(ItemHelper.IsCheckedProperty);
        }

        public static readonly DependencyProperty ParentProperty = DependencyProperty.RegisterAttached("Parent", typeof(object), typeof(ItemHelper));

        public static void SetParent(DependencyObject element, object Parent)
        {
            element.SetValue(ItemHelper.ParentProperty, Parent);
        }

        public static object GetParent(DependencyObject element)
        {
            return (object)element.GetValue(ItemHelper.ParentProperty);
        }
    }

}
