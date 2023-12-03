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
            var publisherVM = UnitOfViewModel.Instance.PublisherViewModel;

            BookISBN bookISBN = bookISBNVM.FindByISBN(sourceItem.ISBN, null);
            BookTitle bookTitle = bookTitleVM.FindById(bookISBN.IdBookTitle);
            Category bookCategory = categoryVM.FindById(bookTitle.IdCategory);
            Author bookAuthor = authorVM.FindById(bookISBN.IdAuthor);
            Author bookTranslator = authorVM.FindById(bookISBN.IdAuthor);
            Publisher publisher = publisherVM.FindById(sourceItem.IdPublisher);

            BookDto newItem = new BookDto(sourceItem.Id);

            newItem.ISBN = bookISBN.ISBN;
            newItem.Title = bookTitle.Name;
            newItem.Category = bookCategory.Name;
            newItem.Author = bookAuthor.Name;
            newItem.Translator = bookTranslator.Name;
            newItem.Language = bookISBN.Language;

            newItem.Publisher = publisher;
            newItem.PublishDate = sourceItem.PublishDate;

            newItem.Price = sourceItem.Price;
            newItem.PriceCurrent  = sourceItem.PriceCurrent;

            newItem.Status = sourceItem.Status;
            newItem.CreatedAt = sourceItem.CreatedAt;
            newItem.ModifiedAt = sourceItem.ModifiedAt;

            return newItem;
        }
    }
}
