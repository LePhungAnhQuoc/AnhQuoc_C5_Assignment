﻿using AnhQuoc_C5_Assignment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using AnhQuoc_C5_Assignment.DTOs.ApiDtos;

namespace AnhQuoc_C5_Assignment
{
    public abstract class Repository<T> : IRepositoryBase<T> where T : class, IMapFromModel, new()
    {
        #region Fields
        protected QuanLyThuVienEntities dbSource;
        protected ObservableCollection<T> _Items;
        protected readonly APIProvider<T> APIProvider;
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
        public Repository(APIProvider<T> ApiProvider)
        {
            this._Items = new ObservableCollection<T>();
            this.APIProvider = ApiProvider;

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

        public void Prepend(T item)
        {
            _Items.Insert(0, item);
        }

        public void Remove(T item)
        {
            _Items.Remove(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _Items.AddRange(collection);
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                _Items.Remove(item);
            }
        }


        public virtual void LoadList()
        {
            _Items = APIProvider.GetAll().ToObservableCollection();
        }


        public virtual void WriteAdd(T item)
        {
            APIProvider.Post(item);
        }

        public virtual void WriteDelete(T item)
        {
            string key = GetPrimaryKeyName();
            APIProvider.Delete(Utilitys.getValueFromProperty(key, item).ToString());
        }

        public virtual void WriteUpdate(T updated)
        {
            string key = GetPrimaryKeyName();
            APIProvider.Put(Utilitys.getValueFromProperty(key, updated).ToString(), updated);
        }


        public virtual void WriteAddRange(ObservableCollection<T> items)
        {
            foreach (T item in items)
            {
                WriteAdd(item);
            }
        }

        public virtual void WriteDeleteRange(ObservableCollection<T> items)
        {
            foreach (T item in items)
            {
                WriteDelete(item);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (T item in _Items)
            {
                yield return item;
            }
        }


        #region PrivateMethods
        private string GetPrimaryKeyName()
        {
            return Utilitys.GetPrimaryKeys(typeof(T)).SingleOrDefault();
        }
        #endregion
    }
}
