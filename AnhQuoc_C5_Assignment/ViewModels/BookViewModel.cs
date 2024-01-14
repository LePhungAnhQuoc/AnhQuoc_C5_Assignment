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
    }
}
