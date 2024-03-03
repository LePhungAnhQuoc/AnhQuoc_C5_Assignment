﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class BookISBNViewModel : BaseViewModel<BookISBN>
    {
        public BookISBNViewModel()
        {
            Item = new BookISBN();
            Repo = new BookISBNRepository();
            prefix = Constants.prefixBookISBN;
            numberPrefix = 2;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.ISBN));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public BookISBN FindByISBN(string isbnValue, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);

            foreach (BookISBN item in source)
            {
                if (string.Compare(item.ISBN, isbnValue, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        public BookISBN Find(string idBookTitle, string language, bool ignoreCase, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);

            foreach (BookISBN item in source)
            {
                if (string.Compare(item.IdBookTitle, idBookTitle, ignoreCase) == 0)
                {
                    if (string.Compare(item.OriginLanguage, language, ignoreCase) == 0)
                    {
                       return item;
                    }
                }
            }
            return null;
        }

        public ObservableCollection<BookISBN> FillByIdBookTitle(string bookTitleId, bool? statusValue)
        {
            return FillByIdBookTitle(Repo.Gets(), bookTitleId, statusValue);
        }

        public ObservableCollection<BookISBN> FillByIdBookTitle(ObservableCollection<BookISBN> source, string bookTitleId, bool? statusValue)
        {
            source = FillByStatus(source, statusValue);

            ObservableCollection<BookISBN> result = new ObservableCollection<BookISBN>();
            foreach (BookISBN item in source)
            {
                if (item.IdBookTitle == bookTitleId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<BookISBN> FillByIdAuthor(ObservableCollection<BookISBN> source, string authorId, bool? statusValue)
        {
            source = FillByStatus(source, statusValue);

            ObservableCollection<BookISBN> result = new ObservableCollection<BookISBN>();
            foreach (BookISBN item in source)
            {
                if (item.IdAuthor == authorId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<string> FillLanguages(ObservableCollection<BookISBN> source, bool? statusValue)
        {
            source = Utilities.FillByStatus(source, statusValue);
            return source.Select(item => item.OriginLanguage).ToObservableCollection();
        }


        public bool IsCheckEmptyItem(BookISBN item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public BookISBN CreateByDto(BookISBNDto dto)
        {
            BookISBN bookISBN = new BookISBN();
            Copy(bookISBN, dto);
            return bookISBN;
        }

        public void Copy(BookISBN dest, BookISBNDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(BookISBNDto dest, BookISBNDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
