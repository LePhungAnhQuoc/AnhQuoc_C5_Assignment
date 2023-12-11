using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookISBNDto: IDtoBase
    {
        public string ISBN { get; set; }

        private BookTitle _BookTitle;
        public BookTitle BookTitle
        {
            get
            {
                return _BookTitle;
            }
            set
            {
                _BookTitle = value;
                OnPropertyChanged();
            }
        }

        private Author _Author;
        public Author Author
        {
            get
            {
                return _Author;
            }
            set
            {
                _Author = value;
                OnPropertyChanged();
            }
        }
        
        private string _OriginLanguage;
        public string OriginLanguage
        {
            get
            {
                return _OriginLanguage;
            }
            set
            {
                _OriginLanguage = value;
                OnPropertyChanged();
            }
        }

        private bool _Status;
        public bool Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Book> _Books;
        public ObservableCollection<Book> Books
        {
            get { return _Books; }
            set
            {
                _Books = value;
                OnPropertyChanged();
            }
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion


        public BookISBNDto(string isbn)
        {
            ISBN = isbn;
            Books = new ObservableCollection<Book>();
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
