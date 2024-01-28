using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserRoleDto : IDtoBase
    {
        public string Id { get; set; }

        private string _IdRole;
        public string IdRole
        {
            get
            {
                return _IdRole;
            }
            set
            {
                _IdRole = value;
                OnPropertyChanged();
            }
        }

        private string _IdUser;
        public string IdUser
        {
            get
            {
                return _IdUser;
            }
            set
            {
                _IdUser = value;
                OnPropertyChanged();
            }
        }


        #region Other-Properties
        private User _User;
        public User User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged();
            }
        }

        private Role _Role;
        public Role Role
        {
            get { return _Role; }
            set
            {
                _Role = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public UserRoleDto(string id)
        {
            Id = id;
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
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
