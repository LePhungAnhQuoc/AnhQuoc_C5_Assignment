using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class BaseViewModel<T> : INotifyPropertyChanged where T : class, IMapFromModel, new()
    {
        #region Fields
        protected string prefix = string.Empty;
        protected int numberPrefix = 2;
        #endregion

        #region Properties
        public T Item { get; set; }
        public Repository<T> Repo { get; set; }
        #endregion
        
        public int getMaxIndexId(string propIdName)
        {
            PropertyInfo propId = Item.GetType().GetProperty(propIdName);
            return getMaxIndexId(propId);
        }

        public int getMaxIndexId(PropertyInfo propId)
        {
            int result = 0;
            foreach (T item in Repo)
            {
                var idValue = propId.GetValue(item).ToString().Where(char.IsDigit);
                string numberStr = string.Join("", idValue);
                int indexId = Convert.ToInt32(numberStr);

                int? getMax = Utilitys.GetMax(result, indexId);
                if (getMax == null)
                {
                    continue;
                }
                result = (int)getMax;
            }
            return result;
        }

        public string GetId(int index)
        {
            string result = string.Empty;
            result = prefix + (index + 1).ToString(string.Format("D{0}", numberPrefix));
            
            return result;
        }

        public ObservableCollection<T> FillByStatus(bool? statusValue)
        {
            return FillByStatus(Repo.Gets(), statusValue);
        }

        public ObservableCollection<T> FillByStatus(ObservableCollection<T> items, bool? statusValue)
        {
            return Utilitys.FillByStatus(items, statusValue);
        }

        public void Copy(T dest, T source)
        {
            Utilitys.Copy(dest, source);
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
