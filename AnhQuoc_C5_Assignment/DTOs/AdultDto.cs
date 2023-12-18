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
    public class AdultDto: IDtoBase
    {
        public string IdReader { get; set; }

        private string _Identify;
        public string Identify
        {
            get
            {
                return _Identify;
            }
            set
            {
                _Identify = value;
                OnPropertyChanged();
            }
        }

        private string _Address;
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }

        private string _City;
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
                OnPropertyChanged();
            }
        }

        private string _Phone;
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ExpireDate;
        public DateTime ExpireDate
        {
            get
            {
                return _ExpireDate;
            }
            set
            {
                _ExpireDate = value;
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

        private string _ReaderName;
        public string ReaderName
        {
            get { return _ReaderName; }
            set
            {
                _ReaderName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Child> _ListChild;
        public ObservableCollection<Child> ListChild
        {
            get
            {
                if (_ListChild == null)
                    _ListChild = new ObservableCollection<Child>();
                return _ListChild;
            }
            set
            {
                _ListChild = value;
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

        public AdultDto(string idReader)
        {
            IdReader = idReader;
            Status = true;
            ListChild = new ObservableCollection<Child>();
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
