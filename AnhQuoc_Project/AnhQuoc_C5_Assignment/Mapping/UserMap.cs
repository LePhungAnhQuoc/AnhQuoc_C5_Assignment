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
            newItem.Username = sourceItem.Username;
            newItem.Password = sourceItem.Password;

            newItem.LName = getInfo.LName;
            newItem.FName = getInfo.FName;

            newItem.Phone = getInfo.Phone;
            newItem.Address = getInfo.Address;


            newItem.Status = sourceItem.Status;
            newItem.CreatedAt = sourceItem.CreatedAt;
            newItem.ModifiedAt = sourceItem.ModifiedAt;

            return newItem;
        }
    }
}
