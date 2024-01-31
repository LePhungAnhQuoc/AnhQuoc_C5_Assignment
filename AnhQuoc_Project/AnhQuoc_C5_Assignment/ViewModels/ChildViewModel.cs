using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ChildViewModel : BaseViewModel<Child>
    {
        public ChildViewModel()
        {
            Item = new Child();
            Repo = new ChildRepository();
            prefix = string.Empty;
            numberPrefix = 0;
        }

        public Child FindByIdReader(string idReader, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);

            foreach (Child child in source)
            {
                if (child.IdReader == idReader)
                {
                    return child;
                }
            }
            return null;
        }

        public ObservableCollection<Child> FillByIdAdult(string idAdult, bool? status)
        {
            ObservableCollection<Child> childsResult = new ObservableCollection<Child>();
            foreach (Child child in Repo)
            {
                if (child.IdAdult == idAdult)
                {
                    childsResult.Add(child);
                }
            }
            childsResult = FillByStatus(childsResult, status);

            return childsResult;
        }


        public ObservableCollection<Child> GetListFromReaders(ObservableCollection<Reader> childReaders, bool? statusValue)
        {
            childReaders = Utilities.FillByStatus(childReaders, statusValue);

            ObservableCollection<Child> newList = new ObservableCollection<Child>();
            foreach (Reader reader in childReaders)
            {
                Child newChild = FindByIdReader(reader.Id, statusValue);
                newList.Add(newChild);
            }
            return newList;
        }

        public ObservableCollection<Child> FillContainsFullName(ObservableCollection<Child> source, string valueName, bool igNoreCase, bool? statusValue)
        {
            ObservableCollection<Child> newList = new ObservableCollection<Child>();
            foreach (Child item in source)
            {
                ReaderViewModel readerVM = UnitOfViewModel.Instance.ReaderViewModel;
                Reader reader = readerVM.FindById(item.IdReader);
                ReaderDto readerDto = (UnitOfMap.Instance.ReaderMap).ConvertToDto(reader);

                if (readerDto.FullName.ContainsCorrectly(valueName, igNoreCase))
                {
                    newList.Add(item);
                }
            }
            newList = FillByStatus(newList, statusValue);
            return newList;
        }

        public ObservableCollection<Child> FillContainsFullName(ObservableCollection<ChildDto> source, string valueName, bool igNoreCase, bool? statusValue)
        {
            return FillContainsFullName(CreateByDto(source), valueName, igNoreCase, statusValue);
        }


        public Child CreateByDto(ChildDto source)
        {
            var result = new Child();
            result.IdReader = source.IdReader;
            Copy(result, source);
            return result;
        }

        public ObservableCollection<Child> CreateByDto(ObservableCollection<ChildDto> source)
        {
            var dest = new ObservableCollection<Child>();
            foreach (ChildDto item in source)
            {
                dest.Add(CreateByDto(item));
            }
            return dest;
        }

        public void Copy(Child dest, ChildDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(ChildDto dest, ChildDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
