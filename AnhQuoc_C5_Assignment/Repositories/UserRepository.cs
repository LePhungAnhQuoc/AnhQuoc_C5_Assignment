using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(): base()
        {
        }

        public UserRepository(ObservableCollection<User> items): base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.Users.ToObservableCollection();
        }

        public override void WriteAdd(User item)
        {
            dbSource.Users.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(User item)
        {
            dbSource.Users.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(User updated)
        {
            var UserViewModel = UnitOfViewModel.Instance.UserViewModel;

            User itemSource = dbSource.Users.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            UserViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(User item, bool status)
        {
            User itemSource = dbSource.Users.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
