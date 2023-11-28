using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookMap: MapBase<Book, BookDto>
    {
        public override BookDto ConvertToDto(Book sourceItem)
        {
            var bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            var bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            var categoryVM = UnitOfViewModel.Instance.CategoryViewModel;
            var authorVM = UnitOfViewModel.Instance.AuthorViewModel;

            BookISBN bookISBN = bookISBNVM.FindByISBN(sourceItem.ISBN, null);
            BookTitle bookTitle = bookTitleVM.FindById(bookISBN.IdBookTitle);
            Category bookCategory = categoryVM.FindById(bookTitle.IdCategory);
            Author bookAuthor = authorVM.FindById(bookTitle.IdAuthor);
            Author bookTranslator = authorVM.FindById(bookISBN.IdAuthor);

            BookDto newItem = new BookDto(sourceItem.Id);

            newItem.ISBN = bookISBN.ISBN;
            newItem.Title = bookTitle.Name;
            newItem.Category = bookCategory.Name;
            newItem.Author = bookAuthor.Name;
            newItem.Translator = bookTranslator.Name;
            newItem.Language = bookISBN.Language;
            newItem.Status = sourceItem.Status;

            newItem.CreatedAt = sourceItem.CreatedAt;
            newItem.ModifiedAt = sourceItem.ModifiedAt;

            return newItem;
        }
    }
}
