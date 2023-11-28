using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class UserViewModel : ViewModelBase<User>
    {
        public UserViewModel()
        {
            Item = new User();
            Repo = new UserRepository();
            prefix = Constants.prefixUser;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public User FindAccount(UserAccountLogIn account)
        {
            foreach (User item in Repo)
            {
                if (string.Compare(account.Username, item.Username, false) == 0
                 && string.Compare(account.Password, item.Password, false) == 0)
                    return item;
            }
            return null;
        }

        public User FindById(string idValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public User FindById(ObservableCollection<User> items, string idValue)
        {
            return items.FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public User FindByUsername(string usernameValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Username, usernameValue, false) == 0);
        }


        public ObservableCollection<User> FillUserRole(ObservableCollection<UserRole> userRoles, bool isHasRole, bool? statusValue)
        {
            var result = Repo.Gets()
                .Join(userRoles, u => u.Id, ur => ur.IdUser, (u, ur) => u).ToObservableCollection();

            if (!isHasRole)
            {
                result = FillExcept(result);
            }
            result = FillByStatus(result, statusValue);
            return result;
        }

        private ObservableCollection<User> FillExcept(ObservableCollection<User> source)
        {
            ObservableCollection<User> getResult = Repo.Gets().Where(u => FindById(source, u.Id) == null).ToObservableCollection();
            return getResult;
        }


        public bool IsCheckEmptyItem(UserDto item)
        {
            if (Utilities.IsCheckEmptyString(item.Username))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.Password))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.LName))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.FName))
            {
                return false;
            }
            if (Utilities.IsCheckEmptyString(item.Phone))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.Address))
            {
                return false;
            }

            return true;
        }

        public User CreateByDto(UserDto dto)
        {
            User user = new User();
            user.Id = dto.Id;
            Copy(user, dto);
            return user;
        }

        public void Copy(User dest, UserDto source)
        {
            dest.Username = source.Username;
            dest.Password = source.Password;

            dest.Status = source.Status;
            dest.CreatedAt = source.ModifiedAt;
            dest.ModifiedAt = source.ModifiedAt;
        }

        public void Copy(UserDto dest, UserDto source)
        {
            dest.Username = source.Username;
            dest.Password = source.Password;

            dest.Status = source.Status;
            dest.CreatedAt = source.ModifiedAt;
            dest.ModifiedAt = source.ModifiedAt;


            dest.LName = source.LName;
            dest.FName = source.FName;

            dest.Phone = source.Phone;
            dest.Address = source.Address;
        }
    }
}
