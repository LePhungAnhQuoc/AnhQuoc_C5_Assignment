using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    class BookISBNToISBNConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            return bookISBNVM.FindByISBN(value.ToString(), null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var isbnValue = value as BookISBN;
            return isbnValue.ISBN;
        }
    }
}
