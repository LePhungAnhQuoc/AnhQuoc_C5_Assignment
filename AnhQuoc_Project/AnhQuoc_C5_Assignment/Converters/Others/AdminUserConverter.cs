using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    class AdminUserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserRoleViewModel userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            UserViewModel userVM = UnitOfViewModel.Instance.UserViewModel;

            string idUser = (string)value;

            var adminRole = userRoleVM.FindByIdRole(Constants.adminRoleId);
            var adminUser = userVM.FindById(adminRole.IdUser);

            if (idUser == adminUser.Id)
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
