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
    public class ReaderRepository : Repository<Reader>
    {
        public ReaderRepository(): base()
        {
        }

        public ReaderRepository(ObservableCollection<Reader> items): base(items)
        {
        }
        
        public override void WriteUpdate(Reader updated)
        {
            var ReaderViewModel = UnitOfViewModel.Instance.ReaderViewModel;

            Reader itemSource = dbSource.Readers.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            ReaderViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Reader item, bool status)
        {
            Reader itemSource = dbSource.Readers.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
