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
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(): base()
        {
        }

        public RoleRepository(ObservableCollection<Role> items) : base(items)
        {
        }
        
        public override void WriteUpdate(Role updated)
        {
            var RoleViewModel = UnitOfViewModel.Instance.RoleViewModel;

            Role itemSource = dbSource.Roles.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            RoleViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Role item, bool status)
        {
            Role itemSource = dbSource.Roles.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
