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
    public class PublisherRepository : Repository<Publisher>
    {
        public PublisherRepository() : base()
        {
        }
        public PublisherRepository(ObservableCollection<Publisher> items) : base(items)
        {
        }

       
        public override void WriteUpdate(Publisher updated)
        {
            var PublisherViewModel = UnitOfViewModel.Instance.PublisherViewModel;

            Publisher itemSource = dbSource.Publishers.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            PublisherViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
