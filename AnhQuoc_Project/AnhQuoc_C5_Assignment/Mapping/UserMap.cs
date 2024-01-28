using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserMap : MapBase<User, UserDto>
    {
        public override UserDto ConvertToDto(User sourceItem)
        {
            var userInfoVM = UnitOfViewModel.Instance.UserInfoViewModel;
            UserInfo getInfo = userInfoVM.FindByIdUser(sourceItem.Id);

            UserDto newItem = new UserDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);
            Utilities.Copy(newItem, getInfo);

            return newItem;
        }
    }
}
