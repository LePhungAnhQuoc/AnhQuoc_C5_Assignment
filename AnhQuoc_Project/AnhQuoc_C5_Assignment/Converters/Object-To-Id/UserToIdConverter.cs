using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    class UserToIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var userVM = UnitOfViewModel.Instance.UserViewModel;
            return userVM.FindById(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var user = value as User;
            return user.Id;
        }
    }
}
