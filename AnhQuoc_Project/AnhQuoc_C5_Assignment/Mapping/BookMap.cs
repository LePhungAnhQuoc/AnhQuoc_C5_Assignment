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
            var translatorVM = UnitOfViewModel.Instance.TranslatorViewModel;
            var publisherVM = UnitOfViewModel.Instance.PublisherViewModel;
            var bookStatusVM = UnitOfViewModel.Instance.BookStatusViewModel;

            BookISBN bookISBN = bookISBNVM.FindByISBN(sourceItem.ISBN, null);
            BookTitle bookTitle = bookTitleVM.FindById(bookISBN.IdBookTitle);
            Category bookCategory = categoryVM.FindById(bookTitle.IdCategory);
            Author bookAuthor = authorVM.FindById(bookISBN.IdAuthor);
            Translator bookTranslator = translatorVM.FindById(sourceItem.IdTranslator);
            Publisher publisher = publisherVM.FindById(sourceItem.IdPublisher);
            BookStatu bookStatus = bookStatusVM.FindById(sourceItem.IdBookStatus);

            BookDto newItem = new BookDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            newItem.ISBN = bookISBN.ISBN;
            newItem.BookTitle = bookTitle;
            newItem.Category = bookCategory;
            newItem.Author = bookAuthor;
            newItem.Translator = bookTranslator;
            newItem.Language = bookISBN.OriginLanguage;
            newItem.Publisher = publisher;
            newItem.BookStatus = bookStatus;
            
            return newItem;
        }
    }
}
