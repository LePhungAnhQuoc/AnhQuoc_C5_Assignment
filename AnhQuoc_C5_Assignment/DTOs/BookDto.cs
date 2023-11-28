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

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set 
            { 
                _Title = value;
                OnPropertyChanged();
            }
        }

        private string _Category;
        public string Category
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

        private string _Author;
        public string Author
        {
            get { return _Author; }
            set 
            { 
                _Author = value; 
                OnPropertyChanged();
            }
        }

        private string _Translator;
        public string Translator
        {
            get { return _Translator; }
            set 
            { 
                _Translator = value; 
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
