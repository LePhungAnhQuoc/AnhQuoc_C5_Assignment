﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanHistoryDto : IDtoBase
    {        
        public string Id { get; set; }

        private string _IdUser;
        public string IdUser
        {
            get { return _IdUser; }
            set
            {
                _IdUser = value;
                OnPropertyChanged();
            }
        }

        private string _IdReader;
        public string IdReader
        {
            get { return _IdReader; }
            set
            {
                _IdReader = value;
                OnPropertyChanged();
            }
        }

        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
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


        private decimal _LoanPaid;
        public decimal LoanPaid
        {
            get { return _LoanPaid; }
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
        
        private decimal _FineMoney;
        public decimal FineMoney
        {
            get { return _FineMoney; }
            set
            {
                _FineMoney = value;
                OnPropertyChanged();
            }
        }
        
        private decimal _Total;
        public decimal Total
        {
            get { return _Total; }
            set
            {
                _Total = value;
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
        
        private DateTime _CreateAt;
        public DateTime CreateAt
        {
            get
            {
                return _CreateAt;
            }
            set
            {
                _CreateAt = value;
                OnPropertyChanged();
            }
        }

        #region Other-Prop
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

        public LoanHistoryDto(string id)
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
