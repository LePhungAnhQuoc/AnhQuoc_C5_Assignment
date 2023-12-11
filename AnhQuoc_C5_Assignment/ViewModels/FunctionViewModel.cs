using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class FunctionViewModel : ViewModelBase<Function>
    {
        public FunctionViewModel()
        {
            Item = new Function();
            Repo = new FunctionRepository();
            prefix = Constants.prefixFunction;
            numberPrefix = 1;
        }

        public string GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return base.GetId(index);
        }

        public Function FindById(string idValue, bool? statusValue = null)
        {
            var source = Repo.Gets();
            source = Utilities.FillByStatus(source, statusValue);
            return FindById(source, idValue);
        }

        public Function FindById(ObservableCollection<Function> items, string idValue)
        {
            return items.FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public ObservableCollection<Function> getFunctionsByListId(IEnumerable<string> lstIdFunc)
        {
            return Repo.Gets().Join(lstIdFunc, f => f.Id, lstId => lstId, (f, lstId) => f)
                .OrderBy(f => Utilities.ExtractNumberFromAString(f.Id)).ToObservableCollection();
        }
        
        public ObservableCollection<Function> getChildsByIdParent(string idParent, bool? status)
        {
            var result = Repo.Gets().Where(item => string.Compare(item.IdParent, idParent, false) == 0).ToObservableCollection();

            result = FillByStatus(result, status);
            return result;
        }
        
        public ObservableCollection<Function> FillParent(ObservableCollection<Function> source, bool? statusValue = null)
        {
            var result = source.Where(item => Utilities.IsCheckEmptyString(item.IdParent));

            result = Utilities.FillByStatus(result.ToObservableCollection(), statusValue);
            return result.ToObservableCollection();
        }

        public ObservableCollection<FunctionDto> FillParent(ObservableCollection<FunctionDto> source, bool? statusValue = null)
        {
            var result = source.Where(item => item.Parent == null);
            result = Utilities.FillByStatus(result.ToObservableCollection(), statusValue);
            return result.ToObservableCollection();
        }


        public ObservableCollection<FunctionDto> FillAdminFunction(ObservableCollection<FunctionDto> source, bool isAdminValue, bool? statusValue = null)
        {
            var result = source.Where(item => item.IsAdmin == isAdminValue);
            result = Utilities.FillByStatus(result.ToObservableCollection(), statusValue);
            return result.ToObservableCollection();
        }

        public ObservableCollection<Function> FillAdminFunction(ObservableCollection<Function> source, bool isAdminValue, bool? statusValue = null)
        {
            var result = source.Where(item => item.IsAdmin == isAdminValue);
            result = Utilities.FillByStatus(result.ToObservableCollection(), statusValue);
            return result.ToObservableCollection();
        }

        public ObservableCollection<FunctionChildDto> FillAdminFunction(ObservableCollection<FunctionChildDto> source, bool isAdminValue, bool? statusValue = null)
        {
            var result = source.Where(item => item.IsAdmin == isAdminValue);
            result = Utilities.FillByStatus(result.ToObservableCollection(), statusValue);
            return result.ToObservableCollection();
        }


        public ObservableCollection<Function> FillByIdParent(ObservableCollection<Function> source, string idParent, bool? statusValue)
        {
            var result = source.Where(f => string.Compare(f.IdParent, idParent, false) == 0).ToObservableCollection();
            result = Utilities.FillByStatus(result.ToObservableCollection(), statusValue);
            return result;
        }
        
        public void FillChildsStatusInParent(ObservableCollection<FunctionDto> items, bool? statusValue)
        {
            foreach (FunctionDto item in items)
            {
                item.Childs = Utilities.FillByStatus(item.Childs, statusValue);
            }
        }

        public void FillChildsAdminInParent(ObservableCollection<FunctionDto> items, bool isAdminValue, bool? statusValue)
        {
            foreach (FunctionDto item in items)
            {
                item.Childs = FillAdminFunction(item.Childs, isAdminValue, statusValue);
                item.Childs = Utilities.FillByStatus(item.Childs, statusValue);
            }
        }


        public bool IsCheckEmptyItem(FunctionDto item)
        {
            if (Utilities.IsCheckEmptyString(item.Name))
            {
                return false;
            }
            return true;
        }
        
        public Function CreateByDto(FunctionDto dto)
        {
            Function function = new Function();
            function.Id = dto.Id;
            Copy(function, dto);
            return function;
        }

        public Function CreateByDto(FunctionChildDto dto)
        {
            Function function = new Function();
            function.Id = dto.Id;
            Copy(function, dto);
            return function;
        }

        public IEnumerable<Function> CreateByDto(ObservableCollection<FunctionChildDto> source)
        {
            IEnumerable<Function> result = source.Select(itemDto => CreateByDto(itemDto));
            return result;
        }

        public void Copy(Function dest, FunctionDto source)
        {
            dest.Name = source.Name;
            dest.Description = source.Description;

            dest.IdParent = (source.Parent == null) ? null : source.Parent.Id;
            dest.UrlImage = source.UrlImage;
            dest.Status = source.Status;
        }

        public void Copy(FunctionChildDto dest, Function source)
        {
            dest.Name = source.Name;
            dest.Description = source.Description;

            dest.IdParent = source.IdParent;
            dest.UrlImage = source.UrlImage;
            dest.IsAdmin = source.IsAdmin;
            dest.Status = source.Status;
        }

        public void Copy(Function dest, FunctionChildDto source)
        {
            dest.Name = source.Name;
            dest.Description = source.Description;

            dest.IdParent = source.IdParent;
            dest.UrlImage = source.UrlImage;
            dest.Status = source.Status;
        }

        public void Copy(FunctionDto dest, FunctionDto source)
        {
            dest.Name = source.Name;
            dest.Description = source.Description;

            dest.Parent = source.Parent;
            dest.UrlImage = source.UrlImage;
            dest.Status = source.Status;
        }
    }
}
