using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailMap : MapBase<LoanDetail, LoanDetailDto>
    {
        public override LoanDetailDto ConvertToDto(LoanDetail sourceItem)
        {
            if (sourceItem == null)
            {
                return null;
            }

            var bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            var bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            var bookVM = UnitOfViewModel.Instance.BookViewModel;

            BookTitle bookTitle = null;
            BookISBN bookISBN = null;
            var book = bookVM.FindById(sourceItem.IdBook, null);

            if (book != null)
            {
                bookISBN = bookISBNVM.FindByISBN(book.ISBN, null);
            }

            if (bookISBN != null)
            {
                bookTitle = bookTitleVM.FindById(bookISBN.IdBookTitle);
            }

            if (bookTitle == null)
            {
                return null;
            }

            LoanDetailDto newItem = new LoanDetailDto(sourceItem.Id);
            Utilitys.Copy(newItem, sourceItem);

            newItem.Book = book;
            newItem.BookTitle = bookTitle;
            newItem.BookISBN = bookISBN;

            return newItem;
        }
    }
}
