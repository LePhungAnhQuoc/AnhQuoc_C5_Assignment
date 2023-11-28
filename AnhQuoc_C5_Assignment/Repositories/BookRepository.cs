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
    public class BookRepository : Repository<Book>
    {
        public BookRepository(): base()
        {
        }

        public BookRepository(ObservableCollection<Book> items): base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.Books.ToObservableCollection();
        }

        public override void WriteAdd(Book item)
        {
            dbSource.Books.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(Book item)
        {
            dbSource.Books.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(Book updated)
        {
            var BookViewModel = UnitOfViewModel.Instance.BookViewModel;

            Book itemSource = dbSource.Books.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            BookViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Book item, bool status)
        {
            Book itemSource = dbSource.Books.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
