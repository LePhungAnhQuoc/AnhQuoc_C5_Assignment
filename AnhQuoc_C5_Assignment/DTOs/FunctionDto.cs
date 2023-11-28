using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class FunctionDto : DependencyObject, IDtoBase
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

        private Function _Parent;
        public Function Parent
        {
            get { return _Parent; }
            set
            {
                _Parent = value;
                OnPropertyChanged();
            }
        }

        public string ParentName
        {
            get
            {
                string result = string.Empty;
                if (Parent != null)
                {
                    result = Parent.Name;
                }
                return result;
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

        private ObservableCollection<FunctionChildDto> _Childs;
        public ObservableCollection<FunctionChildDto> Childs
        {
            get { return _Childs; }
            set
            {
                _Childs = value;
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

        public FunctionDto(string id)
        {
            Id = id;
            Description = string.Empty;
            Status = true;

            Childs = new ObservableCollection<FunctionChildDto>();
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
