﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BookTitleDto : IDtoBase
    {
        public string Id { get; set; }

        private string _IdCategory;
        public string IdCategory
        {
            get { return _IdCategory; }
            set
            {
                _IdCategory = value;
                OnPropertyChanged();
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }
        
        private string _Summary;
        public string Summary
        {
            get
            {
                return _Summary;
            }
            set
            {
                _Summary = value;
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

        private string _UrlImage;
        public string UrlImage
        {
            get { return _UrlImage; }
            set
            {
                _UrlImage = value;
                OnPropertyChanged();
            }
        }


        #region Other-Properties

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

        private ObservableCollection<BookISBN> _BookISBNs;
        public ObservableCollection<BookISBN> BookISBNs
        {
            get { return _BookISBNs; }
            set
            {
                _BookISBNs = value;
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


        public BookTitleDto(string id)
        {
            Id = id;
            BookISBNs = new ObservableCollection<BookISBN>();
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
