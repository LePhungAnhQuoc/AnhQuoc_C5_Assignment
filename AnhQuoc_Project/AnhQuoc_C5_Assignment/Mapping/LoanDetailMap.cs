using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailMap : MapBase<LoanDetail, LoanDetailDto>
    {
        public override LoanDetailDto ConvertToDto(LoanDetail sourceItem)
        {
            var loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            var bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            var bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            var bookVM = UnitOfViewModel.Instance.BookViewModel;

            var book = bookVM.FindById(sourceItem.IdBook, null);
            var bookISBN = bookISBNVM.FindByISBN(book.ISBN, null);

            var bookTitle = bookTitleVM.FindById(bookISBN.IdBookTitle);

            LoanDetailDto newItem = new LoanDetailDto(sourceItem.Id);
            newItem.IdLoan = sourceItem.IdLoan;
            newItem.IdBook = sourceItem.IdBook;
            newItem.ExpDate = sourceItem.ExpDate;

            newItem.Book = book;
            newItem.BookTitle = bookTitle;
            newItem.BookISBN = bookISBN;

            return newItem;
        }
    }
}
