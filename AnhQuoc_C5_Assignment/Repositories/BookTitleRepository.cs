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
    public class BookTitleRepository : Repository<BookTitle>
    {
        public BookTitleRepository() : base()
        {
        }
        public BookTitleRepository(ObservableCollection<BookTitle> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.BookTitles.ToObservableCollection();
        }

        public override void WriteAdd(BookTitle item)
        {
            dbSource.BookTitles.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(BookTitle item)
        {
            dbSource.BookTitles.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(BookTitle updated)
        {
            var BookTitleViewModel = UnitOfViewModel.Instance.BookTitleViewModel;

            BookTitle itemSource = dbSource.BookTitles.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            BookTitleViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
