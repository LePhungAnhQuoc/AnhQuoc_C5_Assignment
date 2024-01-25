using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ChildDto: IDtoBase
    {
        public string IdReader { get; set; }

        private string _IdAdult;
        public string IdAdult
        {
            get { return _IdAdult; }
            set
            {
                _IdAdult = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get
            {
                if (Reader == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Reader.LName + " " + Reader.FName;
                }
            }
        }

        public string AdultName
        {
            get
            {
                if (AdultReader == null)
                {
                    return string.Empty;
                }
                else
                {
                    return AdultReader.LName + " " + AdultReader.FName;
                }
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

        private Reader _Reader;
        public Reader Reader
        {
            get { return _Reader; }
            set
            {
                _Reader = value;
                OnPropertyChanged();
            }
        }

        private Reader _AdultReader;
        public Reader AdultReader
        {
            get { return _AdultReader; }
            set
            {
                _AdultReader = value;
                OnPropertyChanged();
            }
        }

        private Adult _Adult;
        public Adult Adult
        {
            get
            {
                return _Adult;
            }
            set
            {
                _Adult = value;
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


        public ChildDto(string idReader)
        {
            IdReader = idReader;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
