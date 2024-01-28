using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookTitleMap : MapBase<BookTitle, BookTitleDto>
    {
        public override BookTitleDto ConvertToDto(BookTitle sourceItem)
        {
            var bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            var bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            var categoryVM = UnitOfViewModel.Instance.CategoryViewModel;
            var authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            Category category = categoryVM.FindById(sourceItem.IdCategory);

            BookTitleDto newItem = new BookTitleDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);
            newItem.Category = category;
            newItem.BookISBNs = bookISBNVM.FillByIdBookTitle(sourceItem.Id, null);

            return newItem;
        }
    }
}
