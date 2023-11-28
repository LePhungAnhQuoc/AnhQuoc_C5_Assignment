using AnhQuoc_C5_Assignment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public abstract class Repository<T> : IRepositoryBase<T> where T : class, new()
    {
        protected QuanLyThuVienEntities dbSource;

        #region Fields
        protected ObservableCollection<T> _Items;
        #endregion

        #region Properties
        public int Count
        {
            get
            {
                return _Items.Count;
            }
        }
        #endregion

        #region Constructors
        public Repository()
        {
            _Items = new ObservableCollection<T>();
            if (DatabaseFirst.Instance != null)
                dbSource = DatabaseFirst.Instance.dbSource;
        }

        public Repository(ObservableCollection<T> items)
        {
            _Items = items;
            if (DatabaseFirst.Instance != null)
                dbSource = DatabaseFirst.Instance.dbSource;
        }
        #endregion
        
        public ObservableCollection<T> Gets()
        {
            return _Items;
        }


        public void Add(T item)
        {
            _Items.Add(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _Items.AddRange(collection);
        }

        public void Remove(T item)
        {
            _Items.Remove(item);
        }


        public abstract void LoadList();

        public virtual void WriteAddRange(ObservableCollection<T> items)
        {
            foreach (T item in items)
            {
                WriteAdd(item);
            }
        }
        public virtual void WriteUpdateStatus(T item, bool status) { }

        public abstract void WriteAdd(T item);


        public abstract void WriteDelete(T item);

        public abstract void WriteUpdate(T updated);
   
        public IEnumerator GetEnumerator()
        {
            foreach (T item in _Items)
            {
                yield return item;
            }
        }
    }
}
