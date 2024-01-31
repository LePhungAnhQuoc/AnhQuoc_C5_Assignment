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
    public class ReaderDto : IDtoBase
    {
        public string Id { get; set; }

        private string _LName;
        public string LName
        {
            get
            {
                return _LName;
            }
            set
            {
                _LName = value;
                OnPropertyChanged();
            }
        }

        private string _FName;
        public string FName
        {
            get
            {
                return _FName;
            }
            set
            {
                _FName = value;
                OnPropertyChanged();
            }
        }

        private ReaderType _ReaderType;
        public ReaderType ReaderType
        {
            get
            {
                return _ReaderType;
            }
            set
            {
                _ReaderType = value;
                OnPropertyChanged();
            }
        }

        private DateTime _boF;
        public DateTime boF
        {
            get
            {
                return _boF;
            }
            set
            {
                _boF = value;
                OnPropertyChanged();
            }
        }

        private int _ChildsQuantity;
        public int ChildsQuantity
        {
            get { return _ChildsQuantity; }
            set
            {
                _ChildsQuantity = value;
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

        #region Others-props
        public string FullName
        {
            get
            {
                return $"{_LName} {_FName}";
            }
        }

        #endregion

        public ReaderDto(string id)
        {
            Id = id;
            Status = true;
            boF = Constants.dateMinValue;
            ChildsQuantity = -1;
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


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
