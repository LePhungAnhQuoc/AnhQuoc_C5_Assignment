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
    public class ChildRepository : Repository<Child>
    {
        public ChildRepository() : base()
        {

        }
        public ChildRepository(ObservableCollection<Child> items) : base(items)
        {
        }
        
        public override void WriteUpdate(Child updated)
        {
            var ChildViewModel = UnitOfViewModel.Instance.ChildViewModel;

            Child itemSource = dbSource.Children.FirstOrDefault(sourceItem => sourceItem.IdReader == updated.IdReader);
            ChildViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Child item, bool status)
        {
            Child itemSource = dbSource.Children.FirstOrDefault(sourceItem => sourceItem.IdReader == item.IdReader);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
