using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class BookViewModel : ViewModelBase<Book>
    {
        public BookViewModel()
        {
            Item = new Book();
            Repo = new BookRepository();
            prefix = Constants.prefixBook;
        }

        public int GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return GetId(index);
        }

        public new int GetId(int index)
        {
            int result = -1;
            result = (index + 1);

            return result;
        }

        public Book FindById(int id, bool? statusValue)
        {
            var source = Repo.Gets();
            source = Utilities.FillByStatus(source, statusValue);
            foreach (Book item in source)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }
        
        public ObservableCollection<Book> FillByBookISBN(string ISBNValue, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);

            ObservableCollection<Book> result = new ObservableCollection<Book>();
            foreach (Book item in source)
            {
                if (item.ISBN == ISBNValue)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
