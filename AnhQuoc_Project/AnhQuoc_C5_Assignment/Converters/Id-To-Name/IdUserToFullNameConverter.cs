using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    public class IdUserToFullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var userVM = UnitOfViewModel.Instance.UserViewModel;
            var userMap = UnitOfMap.Instance.UserMap;

            User user = userVM.FindById(value.ToString());
            UserDto userDto = userMap.ConvertToDto(user);

            return userDto.FullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
