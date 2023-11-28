using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ChildViewModel : ViewModelBase<Child>
    {
        public ChildViewModel()
        {
            Item = new Child();
            Repo = new ChildRepository();
            prefix = string.Empty;
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

        public ObservableCollection<Child> GetChildFromAdult(string idAdult, bool? status)
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

        public Child CreateByDto(ChildDto source)
        {
            var result = new Child();
            result.IdReader = source.IdReader;
            Copy(result, source);
            return result;
        }

        public void Copy(Child dest, ChildDto source)
        {
            dest.IdReader = source.IdReader;
            dest.IdAdult = source.Adult.IdReader;

            dest.Status = source.Status;
            dest.CreatedAt = source.CreatedAt;
            dest.ModifiedAt = source.ModifiedAt;
        }
    }
}
