using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookStatusMap : MapBase<BookStatu, BookStatusDto>
    {
        public override BookStatusDto ConvertToDto(BookStatu sourceItem)
        {
            BookStatusDto newItem = new BookStatusDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            return newItem;
        }
    }
}
