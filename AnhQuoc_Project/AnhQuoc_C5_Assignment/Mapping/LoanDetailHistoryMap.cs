using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailHistoryMap : MapBase<LoanDetailHistory, LoanDetailHistoryDto>
    {
        public override LoanDetailHistoryDto ConvertToDto(LoanDetailHistory sourceItem)
        {
            var bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            var bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            var bookVM = UnitOfViewModel.Instance.BookViewModel;

            var book = bookVM.FindById(sourceItem.IdBook, null);
            var bookISBN = bookISBNVM.FindByISBN(book.ISBN, null);
            var bookTitle = bookTitleVM.FindById(bookISBN.IdBookTitle);

            LoanDetailHistoryDto newItem = new LoanDetailHistoryDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            newItem.Book = book;
            newItem.BookTitle = bookTitle;
            newItem.BookISBN = bookISBN;

            return newItem;
        }
    }
}
