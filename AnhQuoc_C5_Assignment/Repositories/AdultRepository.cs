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
    public class AdultRepository : Repository<Adult>
    {
        public AdultRepository() : base()
        {
        }

        public AdultRepository(ObservableCollection<Adult> items) : base(items)
        {
        }
        
        public override void WriteUpdate(Adult updated)
        {
            var adultViewModel = UnitOfViewModel.Instance.AdultViewModel;

            Adult itemSource = dbSource.Adults.FirstOrDefault(sourceItem => sourceItem.IdReader == updated.IdReader);
            adultViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Adult item, bool status)
        {
            Adult itemSource = dbSource.Adults.FirstOrDefault(sourceItem => sourceItem.IdReader == item.IdReader);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
