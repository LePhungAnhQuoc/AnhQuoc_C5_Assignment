using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailDto : IDtoBase
    {
        public string Id { get; set; }
        public string IdLoan { get; set; }

        private int _IdBook;
        public int IdBook
        {
            get { return _IdBook; }
            set
            {
                _IdBook = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ExpDate;
        public DateTime ExpDate
        {
            get { return _ExpDate; }
            set
            {
                _ExpDate = value;
                OnPropertyChanged();
            }
        }


        #region OtherProperties
        private BookTitle _BookTitle;
        public BookTitle BookTitle
        {
            get { return _BookTitle; }
            set
            {
                _BookTitle = value;
                OnPropertyChanged();
            }
        }

        private BookISBN _BookISBN;
        public BookISBN BookISBN
        {
            get { return _BookISBN; }
            set
            {
                _BookISBN = value;
                OnPropertyChanged();
            }
        }


        private Book _Book;
        public Book Book
        {
            get { return _Book; }
            set
            {
                _Book = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public LoanDetailDto(string id)
        {
            Id = id;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
