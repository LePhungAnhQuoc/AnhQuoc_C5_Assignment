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
    public class RoleFunctionRepository : Repository<RoleFunction>
    {
        public RoleFunctionRepository()
        {
        }

        public RoleFunctionRepository(ObservableCollection<RoleFunction> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.RoleFunctions.ToObservableCollection();
        }

        public override void WriteAdd(RoleFunction item)
        {
            dbSource.RoleFunctions.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(RoleFunction item)
        {
            dbSource.RoleFunctions.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(RoleFunction updated)
        {
            var RoleFunctionViewModel = UnitOfViewModel.Instance.RoleFunctionViewModel;

            RoleFunction itemSource = dbSource.RoleFunctions.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            RoleFunctionViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
