using AnhQuoc_C5_Assignment;
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

        public virtual void WriteAddRange(ObservableCollection<T> items)
        {
            foreach (T item in items)
            {
                WriteAdd(item);
            }
        }

        public virtual void LoadList()
        {
            PropertyInfo tableProperty = Utilities.GetTablePropertyFromDatabase<T>(dbSource);
            var dbTable = (System.Data.Entity.DbSet<T>) Utilities.getValueFromProperty(tableProperty, dbSource);
            
            _Items = dbTable.ToObservableCollection();
        }

        public virtual void WriteAdd(T item)
        {
            PropertyInfo tableProperty = Utilities.GetTablePropertyFromDatabase<T>(dbSource);
            var getValue = (System.Data.Entity.DbSet<T>)Utilities.getValueFromProperty(tableProperty, dbSource);
            getValue.Add(item);

            dbSource.SaveChanges();
        }
        
        public virtual void WriteDelete(T item)
        {
            PropertyInfo tableProperty = Utilities.GetTablePropertyFromDatabase<T>(dbSource);
            var getValue = (System.Data.Entity.DbSet<T>)Utilities.getValueFromProperty(tableProperty, dbSource);

            getValue.Remove(item);
            dbSource.SaveChanges();
        }

        public virtual void WriteUpdate(T updated)
        {
            string key = string.Empty;
            using (SqlConnection conn = new SqlConnection(Constants.ShortConnStr))
            {
                key = Utilities.GetPrimaryKeys(conn, typeof(T).Name).SingleOrDefault();
            }

            Func<T, bool> predicate = (sourceItem) =>
            {
                return Utilities.getValueFromProperty(key, sourceItem).Equals(Utilities.getValueFromProperty(key, updated));
            };

            PropertyInfo tableProperty = Utilities.GetTablePropertyFromDatabase<T>(dbSource);
            var dbTable = (System.Data.Entity.DbSet<T>)Utilities.getValueFromProperty(tableProperty, dbSource);


            T itemSource = dbTable.FirstOrDefault(predicate);
            Utilities.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }


        public virtual void WriteUpdateStatus(T updated, bool status)
        {
            string key = string.Empty;
            using (SqlConnection conn = new SqlConnection(Constants.ShortConnStr))
            {
                key = Utilities.GetPrimaryKeys(conn, typeof(T).Name).SingleOrDefault();
            }

            Func<T, bool> predicate = (sourceItem) =>
            {
                return Utilities.getValueFromProperty(key, sourceItem).Equals(Utilities.getValueFromProperty(key, updated));
            };

            PropertyInfo tableProperty = Utilities.GetTablePropertyFromDatabase<T>(dbSource);
            var dbTable = (System.Data.Entity.DbSet<T>)Utilities.getValueFromProperty(tableProperty, dbSource);


            T itemSource = dbTable.FirstOrDefault(predicate);
            Utilities.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }


        public IEnumerator GetEnumerator()
        {
            foreach (T item in _Items)
            {
                yield return item;
            }
        }
    }
}
