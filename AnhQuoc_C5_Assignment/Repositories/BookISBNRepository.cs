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
    public class BookISBNRepository : Repository<BookISBN>
    {
        public BookISBNRepository() : base()
        {
        }
        public BookISBNRepository(ObservableCollection<BookISBN> items) : base(items)
        {
        }
        
        public override void WriteUpdate(BookISBN updated)
        {
            var bookISBNViewModel = UnitOfViewModel.Instance.BookISBNViewModel;

            BookISBN itemSource = dbSource.BookISBNs.FirstOrDefault(sourceItem => sourceItem.ISBN == updated.ISBN);
            bookISBNViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(BookISBN item, bool status)
        {
            BookISBN itemSource = dbSource.BookISBNs.FirstOrDefault(sourceItem => sourceItem.ISBN == item.ISBN);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
