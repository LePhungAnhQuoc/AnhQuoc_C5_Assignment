using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace AnhQuoc_C5_Assignment
{
    public class BookISBNViewModel : BaseViewModel<BookISBN>
    {
        public BookISBNViewModel()
        {
            Item = new BookISBN();
            Repo = new BookISBNRepository(new APIProvider<BookISBN>(nameof(BookISBN)));
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

        public BookISBN MostOfISBN(ObservableCollection<LoanSlip> loans)
        {
            Dictionary<string, int> collects = new Dictionary<string, int>();

            LoanDetailViewModel loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;
            BookViewModel bookVM = UnitOfViewModel.Instance.BookViewModel;
            loans.ForEach(item =>
            {
                var loanDetails = loanDetailVM.FillListByIdLoan(item.Id);
                var books = bookVM.GetBooksInLoanDetails(loanDetails);
                var isbnsInBooks = books.Select(book => book.ISBN).ToObservableCollection();
                var newDic = Utilitys.GetOccurrenceInArray(isbnsInBooks);

                newDic.ForEach(key =>
                {
                    var check = collects.FirstOrDefault(itemCollect => itemCollect.Key == key.Key);
                    if (check.Equals(default(KeyValuePair<string, int>))) // khong co
                    {
                        collects.Add(key.Key, key.Value);
                    }
                    else
                    {
                        collects.Remove(check.Key);
                        collects.Add(check.Key, check.Value + key.Value);
                    }
                });
            });
            KeyValuePair<string, int> maxValue = default(KeyValuePair<string, int>);
            collects.ForEach(itemCollect =>
            {
                if (maxValue.Equals(default(KeyValuePair<string, int>)))
                {
                    maxValue = itemCollect;
                }
                else
                {
                    if (maxValue.Value < itemCollect.Value)
                        maxValue = itemCollect;
                }
            });
            return FindByISBN(maxValue.Key, null);
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
            return FillByIdBookTitle(Repo.Gets(), bookTitleId, statusValue);
        }

        public ObservableCollection<BookISBN> FillByIdBookTitle(ObservableCollection<BookISBN> source, string bookTitleId, bool? statusValue)
        {
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

        public ObservableCollection<BookISBN> FillByIdAuthor(ObservableCollection<BookISBN> source, string authorId, bool? statusValue)
        {
            source = FillByStatus(source, statusValue);

            ObservableCollection<BookISBN> result = new ObservableCollection<BookISBN>();
            foreach (BookISBN item in source)
            {
                if (item.IdAuthor == authorId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<string> FillLanguages(ObservableCollection<BookISBN> source, bool? statusValue)
        {
            source = Utilitys.FillByStatus(source, statusValue);
            return source.Select(item => item.OriginLanguage).ToObservableCollection();
        }

        public ObservableCollection<BookISBN> GetISBNsFromBooks(ObservableCollection<Book> books)
        {
            return books.Select(book => FindByISBN(book.ISBN, null)).Distinct().ToObservableCollection();
        }

        public bool IsCheckEmptyItem(BookISBN item)
        {
            return Utilitys.IsCheckEmptyItem(item, false);
        }

        public BookISBN CreateByDto(BookISBNDto dto)
        {
            BookISBN bookISBN = new BookISBN();
            Copy(bookISBN, dto);
            return bookISBN;
        }

        public void Copy(BookISBN dest, BookISBNDto source)
        {
            Utilitys.Copy(dest, source);
        }

        public void Copy(BookISBNDto dest, BookISBNDto source)
        {
            Utilitys.Copy(dest, source);
        }
    }
}
