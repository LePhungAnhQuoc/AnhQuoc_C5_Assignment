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
    public class UserRoleRepository : Repository<UserRole>
    {
        public UserRoleRepository(): base()
        {
        }

        public UserRoleRepository(ObservableCollection<UserRole> items) : base(items)
        {
        }
        
        public override void WriteUpdate(UserRole updated)
        {
            var UserRoleViewModel = UnitOfViewModel.Instance.UserRoleViewModel;

            UserRole itemSource = dbSource.UserRoles.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            UserRoleViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
