using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class PenaltyReasonPaid : IDtoBase
    {
        private PenaltyReason _Reason;

        public PenaltyReason Reason
        {
            get { return _Reason; }
            set
            {
                _Reason = value;
                OnPropertyChanged();
            }
        }


        private bool _IsPaided;
        public bool IsPaided
        {
            get { return _IsPaided; }
            set
            {
                _IsPaided = value;
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

        public PenaltyReasonPaid()
        {
            IsPaided = false;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
