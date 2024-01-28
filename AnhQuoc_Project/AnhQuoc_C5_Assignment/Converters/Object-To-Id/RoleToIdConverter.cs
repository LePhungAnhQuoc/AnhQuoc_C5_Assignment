using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    class RoleToIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var roleVM = UnitOfViewModel.Instance.RoleViewModel;

            return roleVM.FindById(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var role = value as Role;
            return role.Id;
        }
    }
}
