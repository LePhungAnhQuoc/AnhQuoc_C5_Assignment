using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ViewModelBase<T> where T : class, new()
    {
        #region Fields
        protected string prefix = string.Empty;
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

                int? getMax = Utilities.GetMax(result, indexId);
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
            result = prefix + (index + 1).ToString();
            
            return result;
        }

        public ObservableCollection<T> FillByStatus(bool? statusValue)
        {
            return FillByStatus(Repo.Gets(), statusValue);
        }

        public ObservableCollection<T> FillByStatus(ObservableCollection<T> items, bool? statusValue)
        {
            return Utilities.FillByStatus(items, statusValue);
        }

        public void Copy(T dest, T source)
        {
            Utilities.Copy(dest, source);
        }

    }
}
