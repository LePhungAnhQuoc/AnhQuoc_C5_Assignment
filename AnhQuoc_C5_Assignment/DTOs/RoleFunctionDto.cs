using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class RoleFunctionDto : IDtoBase
    {
        public string Id { get; set; }
        
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

        private Function _Function;
        public Function Function
        {
            get { return _Function; }
            set
            {
                _Function = value;
                OnPropertyChanged();
            }
        }
        
        public RoleFunctionDto(string id)
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
