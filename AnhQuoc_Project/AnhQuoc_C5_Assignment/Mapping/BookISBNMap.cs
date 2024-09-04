using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookISBNMap: MapBase<BookISBN, BookISBNDto>
    {
        public override BookISBNDto ConvertToDto(BookISBN sourceItem)
        {
            var authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            var bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            var bookVM = UnitOfViewModel.Instance.BookViewModel;

            BookISBNDto newItem = new BookISBNDto(sourceItem.ISBN);
            Utilitys.Copy(newItem, sourceItem);

            newItem.BookTitle = bookTitleVM.FindById(sourceItem.IdBookTitle);
            newItem.Author = authorVM.FindById(sourceItem.IdAuthor);
            newItem.Books = bookVM.FillByBookISBN(sourceItem.ISBN, null);
            return newItem;
        }
    }
}
