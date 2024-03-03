using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class UserRoleViewModel : BaseViewModel<UserRole>
    {
        public UserRoleViewModel()
        {
            Item = new UserRole();
            Item.Id = string.Empty;
            Repo = new UserRoleRepository();
            prefix = Constants.prefixUserRole;
            numberPrefix = 1;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public UserRole FindById(string idValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public UserRole FindByIdUser(string idUserValue)
        {
            return Repo.Gets()
                .FirstOrDefault(item => string.Compare(item.IdUser, idUserValue, false) == 0);
        }

        public UserRole FindByIdRole(string idRoleValue)
        {
            return Repo.Gets()
                .FirstOrDefault(item => string.Compare(item.IdRole, idRoleValue, false) == 0);
        }

        public ObservableCollection<UserRole> FillByRoleGroup(RoleViewModel roleVM, string roleGroup)
        {
            return Repo.Gets().Where(i =>
            {
                Role role = roleVM.FindById(i.IdRole);
                return string.Compare(role.Group, roleGroup, false) == 0;
            }).ToObservableCollection();
        }
   
        public ObservableCollection<UserRole> FillByIdRole(string idRole)
        {
            return Repo.Gets().Where(item => string.Compare(item.IdRole, idRole) == 0).ToObservableCollection();
        }

        public ObservableCollection<UserRole> FillByIdUser(string idUser)
        {
            return Repo.Gets().Where(item => string.Compare(item.IdUser, idUser) == 0).ToObservableCollection();
        }


        public bool IsCheckEmptyItem(UserRoleDto item)
        {
            if (item.User == null)
            {
                return false;
            }
            if (item.Role == null)
            {
                return false;
            }
            return true;
        }

        public UserRole CreateByDto(UserRoleDto dto)
        {
            UserRole userRole = new UserRole();
            userRole.Id = dto.Id;
            Copy(userRole, dto);
            return userRole;
        }

        public void Copy(UserRole dest, UserRoleDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(UserRoleDto dest, UserRoleDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
