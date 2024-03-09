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
    public class AdultViewModel: BaseViewModel<Adult>
    {
        public AdultViewModel()
        {
            Item = new Adult();
            Repo = new AdultRepository();
            prefix = string.Empty;
            numberPrefix = 0;
        }
        
        public Adult FindByIdReader(string idReader, bool? statusValue = null)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);
            return source.FirstOrDefault(item => item.IdReader == idReader);
        }

        public Adult FindByIdentify(string identifyValue, bool? statusValue)
        {
            return FindByIdentify(Repo.Gets(), identifyValue, statusValue);
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

        public ObservableCollection<Adult> GetListFromChilds(ObservableCollection<Child> childs, bool? statusValue)
        {
            var readerVM = (UnitOfViewModel.Instance.ReaderViewModel);
            var childVM = (UnitOfViewModel.Instance.ChildViewModel);

            var childReaders = readerVM.GetListFromChilds(childs);
            childReaders = Utilities.FillByStatus(childReaders, statusValue);
            childs = childVM.GetListFromReaders(childReaders, statusValue);

            ObservableCollection<Adult> newList = new ObservableCollection<Adult>();
            foreach (Child child in childs)
            {
                Adult newAdult = FindByIdReader(child.IdAdult, statusValue);

                if (!newList.Contains(newAdult))
                    newList.Add(newAdult);
            }
            return newList;
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

        public ObservableCollection<Adult> FillContainsIdentify(ObservableCollection<AdultDto> source, string valueName, bool igNoreCase, bool? statusValue)
        {
            return FillContainsIdentify(CreateByDto(source), valueName, igNoreCase, statusValue);
        }

        public ObservableCollection<Adult> FillByCity(ObservableCollection<Adult> source, string cityValue, bool? statusValue)
        {
            source = FillByStatus(source, statusValue);

            ObservableCollection<Adult> result = new ObservableCollection<Adult>();
            foreach (Adult item in source)
            {
                if (item.City == cityValue)
                {
                    result.Add(item);
                }
            }
            return result;
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

            if (item.ExpireDate == Constants.dateEmptyValue)
            {
                return false;
            }

            return true;
        }

        public void Copy(Adult dest, AdultDto source)
        {
            Utilities.Copy(dest, source);
        }

        public Adult CreateByDto(AdultDto source)
        {
            var result = new Adult();
            result.IdReader = source.IdReader;

            Copy(result, source);
            return result;
        }

        public ObservableCollection<Adult> CreateByDto(ObservableCollection<AdultDto> source)
        {
            var dest = new ObservableCollection<Adult>();
            foreach (AdultDto item in source)
            {
                dest.Add(CreateByDto(item));
            }
            return dest;
        }
    }
}
