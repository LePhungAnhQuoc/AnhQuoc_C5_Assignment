using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookStatusViewModel: BaseViewModel<BookStatu>
    {
        public BookStatusViewModel()
        {
            Item = new BookStatu();
            Repo = new BookStatusRepository(new APIProvider<BookStatu>("BookStatus"));
            prefix = Constants.prefixBookStatus;
            numberPrefix = 1;
        }

        public string GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return base.GetId(index);
        }

        public BookStatu FindById(string idValue)
        {
            return FindById(Repo.Gets(), idValue);
        }

        public BookStatu FindById(ObservableCollection<BookStatu> source, string idValue)
        {
            return source.FirstOrDefault(item => item.Id == idValue);

        }


        public bool IsCheckEmptyItem(BookStatusDto item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public BookStatu CreateByDto(BookStatusDto dto)
        {
            BookStatu bookStatus = new BookStatu();
            Copy(bookStatus, dto);
            return bookStatus;
        }

        public string[] FillName()
        {
            return Repo.Gets().Select(item => item.Name).ToArray();
        }

        public void Copy(BookStatu dest, BookStatusDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(BookStatusDto dest, BookStatusDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
