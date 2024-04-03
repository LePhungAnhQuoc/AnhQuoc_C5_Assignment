using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class StatisticalDto : IDtoBase
    {
        public System.DateTime DateTime { get; set; }

        private string _Data;
        public string Data
        {
            get { return _Data; }
            set 
            {
                _Data = value;
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


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public StatisticalDto(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
