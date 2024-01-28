using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserDto : IDtoBase
    {
        public string Id { get; set; }

        public string IdUser
        {
            get
            {
                return this.Id;
            }
            set
            {
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged();
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        private string _LName;
        public string LName
        {
            get { return _LName; }
            set
            {
                _LName = value;
                OnPropertyChanged();
            }
        }

        private string _FName;
        public string FName
        {
            get { return _FName; }
            set
            {
                _FName = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get
            {
                return LName + " " + FName;
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
            get { return _Status; }
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

        public UserDto(string id)
        {
            Id = id;
            Status = true;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
