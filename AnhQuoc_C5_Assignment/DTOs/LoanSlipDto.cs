using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanSlipDto : IDtoBase
    {
        public string Id { get; set; }
        
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

        private int _Quantity;
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged();
            }
        }


        private DateTime _LoanDate;
        public DateTime LoanDate
        {
            get { return _LoanDate; }
            set
            {
                _LoanDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ExpDate;
        public DateTime ExpDate
        {
            get { return _ExpDate; }
            set
            {
                _ExpDate = value;
                OnPropertyChanged();
            }
        }


        public decimal _LoanPaid;
        public decimal LoanPaid
        {
            get
            {
                return _LoanPaid;
            }
            set
            {
                _LoanPaid = value;
                OnPropertyChanged();
            }
        }

        private decimal _Deposit;
        public decimal Deposit
        {
            get { return _Deposit; }
            set
            {
                _Deposit = value;
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

        public LoanSlipDto(string id)
        {
            Id = id;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
