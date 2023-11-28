using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class FunctionChildDto : DependencyObject, IDtoBase
    {
        public string Id { get; set; }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged();
            }
        }

        private string _IdParent;
        public string IdParent
        {
            get { return _IdParent; }
            set
            {
                _IdParent = value;
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


        private bool _IsAdmin;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                _IsAdmin = value;
                OnPropertyChanged();
            }
        }


        private bool _Status;
        public bool Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSeal;
        public bool IsSeal
        {
            get { return _IsSeal; }
            set
            {
                _IsSeal = value;
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

        public FunctionChildDto()
        {
            Status = true;
            IsSeal = false;
        }
        public FunctionChildDto(string id)
        {
            Id = id;
            Status = true;
            IsSeal = false;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
