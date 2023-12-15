using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class EnrollDto : IDtoBase
    {        
        public string Id { get; set; }

        private string _ISBN;
        public string ISBN
        {
            get { return _ISBN; }
            set
            {
                _ISBN = value;
                OnPropertyChanged();
            }
        }

        private string _IdReader;
        public string IdReader
        {
            get { return _IdReader; }
            set
            {
                _IdReader = value;
                OnPropertyChanged();
            }
        }

        private Reader _Reader;
        public Reader Reader
        {
            get { return _Reader; }
            set
            {
                _Reader = value;
                OnPropertyChanged();
            }
        }


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


        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                OnPropertyChanged();
            }
        }

        private DateTime _EnrollDate;
        public DateTime EnrollDate
        {
            get { return _EnrollDate; }
            set
            {
                _EnrollDate = value;
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


        private string _Note;
        public string Note
        {
            get { return _Note; }
            set
            {
                _Note = value;
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

        public EnrollDto(string id)
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
