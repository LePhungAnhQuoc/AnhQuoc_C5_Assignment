using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookStatusRepository : Repository<BookStatu>
    {
        public BookStatusRepository()
        {
        }

        public BookStatusRepository(ObservableCollection<BookStatu> items) : base(items)
        {
        }
    }
}
