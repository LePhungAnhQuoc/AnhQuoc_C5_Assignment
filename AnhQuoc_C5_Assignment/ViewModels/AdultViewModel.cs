using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AdultViewModel: ViewModelBase<Adult>
    {
        public AdultViewModel()
        {
            Item = new Adult();
            Repo = new AdultRepository();
            prefix = string.Empty;
        }
        
        public Adult FindByIdReader(string idReader, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);
            return source.FirstOrDefault(item => item.IdReader == idReader);
        }
        
        public Adult FindByIdentify(ObservableCollection<Adult> items, string identifyValue, bool? statusValue)
        {
            items = FillByStatus(items, statusValue);

            return items.FirstOrDefault(item => item.Identify == identifyValue);
        }

        public ObservableCollection<AdultDto> FillByChildsQuantityRule(ObservableCollection<AdultDto> items, int maxChildsQuantity, bool? statusValue)
        {
            items = Utilities.FillByStatus(items, statusValue);

            ObservableCollection<AdultDto> result = new ObservableCollection<AdultDto>();
            foreach (AdultDto item in items)
            {
                if (item.ListChild.Count <= maxChildsQuantity)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ObservableCollection<Adult> GetListFromReaders(ObservableCollection<Reader> adultReaders, bool? statusValue)
        {
            adultReaders = Utilities.FillByStatus(adultReaders, statusValue);

            ObservableCollection<Adult> newList = new ObservableCollection<Adult>();
            foreach (Reader reader in adultReaders)
            {
                Adult newAdult = FindByIdReader(reader.Id, statusValue);
                newList.Add(newAdult);
            }
            return newList;
        }

        public ObservableCollection<Adult> FillContainsIdentify(ObservableCollection<Adult> source, string valueName, bool igNoreCase, bool? statusValue)
        {
            ObservableCollection<Adult> newList = new ObservableCollection<Adult>();
            foreach (Adult item in source)
            {
                if (item.Identify.ContainsCorrectly(valueName, igNoreCase))
                {
                    newList.Add(item);
                }
            }
            newList = FillByStatus(newList, statusValue);
            return newList;
        }

        public DateTime GetExpireDate(Parameter parameter, DateTime createdAt)
        {
            int value = Convert.ToInt32(parameter.Value.ToString());
            DateTime result = createdAt.AddMonths(value);
            return result;
        }


        public bool IsCheckEmptyItem(Adult item)
        {
            if (Utilities.IsCheckEmptyString(item.Identify))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.Address))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.City))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.Phone))
            {
                return false;
            }

            if (item.ExpireDate == null)
            {
                return false;
            }
            return true;
        }
    }
}
