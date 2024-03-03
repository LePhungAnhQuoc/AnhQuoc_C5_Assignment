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
    public class BookViewModel : BaseViewModel<Book>
    {
        public BookViewModel()
        {
            Item = new Book();
            Repo = new BookRepository();
            prefix = Constants.prefixBook;
            numberPrefix = 0;
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
            return FindById(Repo.Gets(), id, statusValue);
        }

        public Book FindById(ObservableCollection<Book> source, int id, bool? statusValue)
        {
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

        public BookDto FindById(ObservableCollection<BookDto> source, int id, bool? statusValue)
        {
            source = Utilities.FillByStatus(source, statusValue);

            foreach (BookDto item in source)
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
            return FillByBookISBN(Repo.Gets(), ISBNValue, statusValue);
        }

        public ObservableCollection<Book> FillByBookISBN(ObservableCollection<Book> source, string ISBNValue, bool? statusValue)
        {
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

        public ObservableCollection<Book> FillByIdPublisher(ObservableCollection<Book> source, string idPublisherValue, bool? statusValue)
        {
            source = FillByStatus(source, statusValue);

            ObservableCollection<Book> result = new ObservableCollection<Book>();
            foreach (Book item in source)
            {
                if (item.IdPublisher == idPublisherValue)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<Book> FillByIdTranslator(ObservableCollection<Book> source, string idTranslatorValue, bool? statusValue)
        {
            source = FillByStatus(source, statusValue);

            ObservableCollection<Book> result = new ObservableCollection<Book>();
            foreach (Book item in source)
            {
                if (item.IdTranslator == idTranslatorValue)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<Book> FillContainsBookTitleName(ObservableCollection<Book> source, string titleValue, bool igNoreCase)
        {
            BookMap bookMap = UnitOfMap.Instance.BookMap;

            ObservableCollection<Book> newList = new ObservableCollection<Book>();
            foreach (Book item in source)
            {
                BookDto itemDto = bookMap.ConvertToDto(item);

                if (itemDto.BookTitle.Name.ContainsCorrectly(titleValue, igNoreCase))
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

        public ObservableCollection<Book> FillByCategory(ObservableCollection<Book> source, string catIdValue, bool? statusValue)
        {
            BookMap bookMap = UnitOfMap.Instance.BookMap;
            source = FillByStatus(source, statusValue);

            ObservableCollection<Book> result = new ObservableCollection<Book>();
            foreach (Book item in source)
            {
                BookDto itemDto = bookMap.ConvertToDto(item);

                if (itemDto.Category.Id == catIdValue)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<Book> GetBooksInLoanSlip(LoanSlip loan)
        {
            var loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;
            var details = loanDetailVM.FillListByIdLoan(loan.Id);

            ObservableCollection<Book> result = GetBooksInLoanDetails(details);
            return result;
        }

        public ObservableCollection<Book> GetBooksInLoanSlips(ObservableCollection<LoanSlip> loans)
        {
            var result = new ObservableCollection<Book>();
            foreach (LoanSlip loan in loans)
            {
                result.AddRange(GetBooksInLoanSlip(loan));
            }
            return result;
        }


        public ObservableCollection<Book> GetBooksInLoanDetails(ObservableCollection<LoanDetail> loanDetails)
        {
            var lstId = loanDetails.Select(item => item.IdBook).ToObservableCollection();
            var books = getListFromIds(lstId);
            return books;
        }

        public ObservableCollection<Book> getListFromIds(ObservableCollection<int> lstId)
        {
            return lstId.Select(item => FindById(item, null)).ToObservableCollection();
        }


        public bool IsCheckEmptyItem(BookDto item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public Book CreateByDto(BookDto dto)
        {
            Book book = new Book();
            Copy(book, dto);
            return book;
        }

        public void Copy(Book dest, BookDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(BookDto dest, BookDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
