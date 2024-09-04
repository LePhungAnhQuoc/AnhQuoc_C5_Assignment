using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
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
    public class DatabaseName : IModelBase, IMapFromModel
    {
        public string Id { get; set; }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
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

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Constructors
        public DatabaseName()
        {
            Name = string.Empty;
        }

        public DatabaseName(string id)
        {
            Id = id;
            Name = string.Empty;
        }

        public DatabaseName(string id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public object MapToAdd()
        {
            throw new NotImplementedException();
        }

        public object MapToUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
