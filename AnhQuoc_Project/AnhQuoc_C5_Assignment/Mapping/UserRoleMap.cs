using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserRoleMap : MapBase<UserRole, UserRoleDto>
    {
        public override UserRoleDto ConvertToDto(UserRole sourceItem)
        {
            var userVM = UnitOfViewModel.Instance.UserViewModel;
            var roleVM = UnitOfViewModel.Instance.RoleViewModel;

            UserRoleDto newItem = new UserRoleDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            newItem.User = userVM.FindById(sourceItem.IdUser);
            newItem.Role = roleVM.FindById(sourceItem.IdRole);
            
            return newItem;
        }
    }
}
