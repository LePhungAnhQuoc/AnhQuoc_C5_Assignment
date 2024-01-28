using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookDto: IDtoBase
    {
        public int Id { get; set; }

        private string _ISBN;
        public string ISBN
        {
            get
            {
                return _ISBN;
            }
            set
            {
                _ISBN = value;
                OnPropertyChanged();
            }
        }

        private string _IdPublisher;
        public string IdPublisher
        {
            get { return _IdPublisher; }
            set
            {
                _IdPublisher = value;
                OnPropertyChanged();
            }
        }

        private string _IdTranslator;
        public string IdTranslator
        {
            get { return _IdTranslator; }
            set
            {
                _IdTranslator = value;
                OnPropertyChanged();
            }
        }

        public string _Language;
        public string Language
        {
            get
            {
                return _Language;
            }
            set 
            { 
                _Language = value;
                OnPropertyChanged();
            }
        }

        private DateTime _PublishDate;
        public DateTime PublishDate
        {
            get { return _PublishDate; }
            set
            {
                _PublishDate = value;
                OnPropertyChanged();
            }
        }

        private string _IdBookStatus;
        public string IdBookStatus
        {
            get { return _IdBookStatus; }
            set
            {
                _IdBookStatus = value;
                OnPropertyChanged();
            }
        }

        private decimal _Price;
        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }

        private decimal _PriceCurrent;
        public decimal PriceCurrent
        {
            get { return _PriceCurrent; }
            set
            {
                _PriceCurrent = value;
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

        private DateTime _CreatedAt;
        public DateTime CreatedAt
        {
            get
            {
                return _CreatedAt;
            }
            set
            {
                _CreatedAt = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ModifiedAt;
        public DateTime ModifiedAt
        {
            get
            {
                return _ModifiedAt;
            }
            set
            {
                _ModifiedAt = value;
                OnPropertyChanged();
            }
        }

        #region Other-Properties

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

        private Category _Category;
        public Category Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
                OnPropertyChanged();
            }
        }

        private Author _Author;
        public Author Author
        {
            get { return _Author; }
            set
            {
                _Author = value;
                OnPropertyChanged();
            }
        }

        private Translator _Translator;
        public Translator Translator
        {
            get { return _Translator; }
            set
            {
                _Translator = value;
                OnPropertyChanged();
            }
        }

        private Publisher _Publisher;
        public Publisher Publisher
        {
            get { return _Publisher; }
            set
            {
                _Publisher = value;
                OnPropertyChanged();
            }
        }

        private BookStatu _BookStatus;
        public BookStatu BookStatus
        {
            get { return _BookStatus; }
            set
            {
                _BookStatus = value;
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

        public BookDto(int id)
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
