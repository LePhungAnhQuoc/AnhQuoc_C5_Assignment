using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class UserViewModel : BaseViewModel<User>
    {
        public UserViewModel()
        {
            Item = new User();
            Repo = new UserRepository(new APIProvider<User>(nameof(User)));
            prefix = Constants.prefixUser;
            numberPrefix = 1;
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
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public User CreateByDto(UserDto dto)
        {
            User user = new User();
            Copy(user, dto);
            return user;
        }

        public void Copy(User dest, UserDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(UserDto dest, UserDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
