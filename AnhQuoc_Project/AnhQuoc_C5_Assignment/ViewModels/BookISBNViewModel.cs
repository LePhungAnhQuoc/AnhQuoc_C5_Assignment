using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class BookISBNViewModel : BaseViewModel<BookISBN>
    {
        public BookISBNViewModel()
        {
            Item = new BookISBN();
            Repo = new BookISBNRepository();
            prefix = Constants.prefixBookISBN;
            numberPrefix = 2;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.ISBN));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public BookISBN FindByISBN(string isbnValue, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);

            foreach (BookISBN item in source)
            {
                if (string.Compare(item.ISBN, isbnValue, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        public BookISBN Find(string idBookTitle, string language, bool ignoreCase, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);

            foreach (BookISBN item in source)
            {
                if (string.Compare(item.IdBookTitle, idBookTitle, ignoreCase) == 0)
                {
                    if (string.Compare(item.OriginLanguage, language, ignoreCase) == 0)
                    {
                       return item;
                    }
                }
            }
            return null;
        }

        public ObservableCollection<BookISBN> FillByIdBookTitle(string bookTitleId, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);

            ObservableCollection<BookISBN> result = new ObservableCollection<BookISBN>();
            foreach (BookISBN item in source)
            {
                if (item.IdBookTitle == bookTitleId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<string> FillLanguages(ObservableCollection<BookISBN> source, bool? statusValue)
        {
            source = Utilities.FillByStatus(source, statusValue);
            return source.Select(item => item.OriginLanguage).ToObservableCollection();
        }
    }
}
