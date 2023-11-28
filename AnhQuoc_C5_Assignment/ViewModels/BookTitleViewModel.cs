using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class BookTitleViewModel : ViewModelBase<BookTitle>
    {
        public BookTitleViewModel()
        {
            Item = new BookTitle();
            Repo = new BookTitleRepository();
            prefix = Constants.prefixBookTitle;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public BookTitle FindById(string idValue)
        {
            foreach (BookTitle bookTitle in Repo)
            {
                if (bookTitle.Id == idValue)
                {
                    return bookTitle;
                }
            }
            return null;
        }
        
        public bool IsCheckEmptyItem(BookTitle item)
        {
            if (Utilities.IsCheckEmptyString(item.Name))
            {
                return false;
            }
            return true;
        }
    }
}
