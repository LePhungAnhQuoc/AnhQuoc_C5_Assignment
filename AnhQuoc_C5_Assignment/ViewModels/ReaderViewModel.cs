using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class ReaderViewModel : ViewModelBase<Reader>
    {
        ReaderMap readerMap;
        public ReaderViewModel()
        {
            Item = new Reader();
            Repo = new ReaderRepository();
            prefix = Constants.prefixReader;

            readerMap = UnitOfMap.Instance.ReaderMap;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public Reader FindById(string id)
        {
            foreach (Reader item in Repo)
            {
                if (string.Compare(item.Id, id, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        public ReaderDto FindById(ObservableCollection<ReaderDto> source, string id)
        {
            foreach (ReaderDto item in source)
            {
                if (string.Compare(item.Id, id, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        public bool IsCheckEmptyItem(Reader item)
        {
            if (Utilities.IsCheckEmptyString(item.FName))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.LName))
            {
                return false;
            }

            return true;
        }

        public ObservableCollection<Reader> FillListByType(ReaderType type, bool? statusValue)
        {
            return FillListByType(Repo.Gets(), type, statusValue);
        }

        public ObservableCollection<Reader> FillListByType(ObservableCollection<Reader> source, ReaderType type, bool? statusValue)
        {
            ObservableCollection<Reader> newList = new ObservableCollection<Reader>();
            foreach (Reader item in source)
            {
                if (item.ReaderType.ConvertValue() == type)
                {
                    newList.Add(item);
                }
            }
            newList = FillByStatus(newList, statusValue);

            return newList;
        }
        
        public ObservableCollection<Reader> FillAdults(bool? status)
        {
            var result = FillListByType(ReaderType.Adult, status);
            return result;
        }

        public ObservableCollection<Reader> FillAdults(ObservableCollection<Reader> source, bool? statusValue)
        {
            return FillListByType(source, ReaderType.Adult, statusValue);
        }

        public ObservableCollection<Reader> FillContainNames(ObservableCollection<Reader> source, string valueName, bool igNoreCase, bool? statusValue)
        {
            ObservableCollection<Reader> newList = new ObservableCollection<Reader>();
            foreach (Reader item in source)
            {
                ReaderDto itemDto = readerMap.ConvertToDto(item);
                if (itemDto.FullName.ContainsCorrectly(valueName, igNoreCase))
                {
                    newList.Add(item);
                }
            }
            newList = FillByStatus(newList, statusValue);
            return newList;
        }

        public ObservableCollection<Reader> GetListFromAdults(ObservableCollection<Adult> adults)
        {
            ObservableCollection<Reader> newList = new ObservableCollection<Reader>();
            foreach (Adult adult in adults)
            {
                Reader newReader = FindById(adult.IdReader);
                newList.Add(newReader);
            }
            return newList;
        }

        public ObservableCollection<Reader> GetListFromAdults(ObservableCollection<AdultDto> adultDtos)
        {
            ObservableCollection<Reader> newList = new ObservableCollection<Reader>();
            foreach (AdultDto adult in adultDtos)
            {
                Reader newReader = FindById(adult.IdReader);
                newList.Add(newReader);
            }
            return newList;
        }
    }
}
