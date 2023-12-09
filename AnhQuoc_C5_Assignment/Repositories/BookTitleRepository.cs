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
        
        public override void WriteUpdate(BookTitle updated)
        {
            var BookTitleViewModel = UnitOfViewModel.Instance.BookTitleViewModel;

            BookTitle itemSource = dbSource.BookTitles.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            BookTitleViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
