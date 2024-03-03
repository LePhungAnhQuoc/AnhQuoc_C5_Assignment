using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class BookTitleViewModel : BaseViewModel<BookTitle>
    {
        public BookTitleViewModel()
        {
            Item = new BookTitle();
            Repo = new BookTitleRepository();
            prefix = Constants.prefixBookTitle;
            numberPrefix = 2;
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

        public ObservableCollection<BookTitle> FillContainsName(ObservableCollection<BookTitle> source, string valueName, bool igNoreCase)
        {
            ObservableCollection<BookTitle> newList = new ObservableCollection<BookTitle>();
            foreach (BookTitle item in source)
            {
                if (item.Name.ContainsCorrectly(valueName, igNoreCase))
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

        public ObservableCollection<BookTitle> FillContainsName(ObservableCollection<BookTitleDto> source, string valueName, bool igNoreCase)
        {
            return FillContainsName(CreateByDto(source), valueName, igNoreCase);
        }

        public ObservableCollection<BookTitle> FillByIdCategory(ObservableCollection<BookTitle> source, string categoryId, bool? statusValue)
        {
            source = FillByStatus(source, statusValue);

            ObservableCollection<BookTitle> result = new ObservableCollection<BookTitle>();
            foreach (BookTitle item in source)
            {
                if (item.IdCategory == categoryId)
                {
                    result.Add(item);
                }
            }
            return result;
        }


        public bool IsCheckEmptyItem(BookTitleDto item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public BookTitle CreateByDto(BookTitleDto source)
        {
            var result = new BookTitle();
            result.Id = source.Id;
            Copy(result, source);
            return result;
        }

        public ObservableCollection<BookTitle> CreateByDto(ObservableCollection<BookTitleDto> source)
        {
            var dest = new ObservableCollection<BookTitle>();
            foreach (BookTitleDto item in source)
            {
                dest.Add(CreateByDto(item));
            }
            return dest;
        }

        public void Copy(BookTitle dest, BookTitleDto source)
        {
            Utilities.Copy(dest, source);
        }


        public void Copy(BookTitleDto dest, BookTitleDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
