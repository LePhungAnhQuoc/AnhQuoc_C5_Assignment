using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ParameterRepository : Repository<Parameter>
    {
        public ParameterRepository() : base()
        {
        }
        public ParameterRepository(ObservableCollection<Parameter> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.Parameters.ToObservableCollection();
        }

        public override void WriteAdd(Parameter item)
        {
            dbSource.Parameters.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(Parameter item)
        {
            dbSource.Parameters.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(Parameter updated)
        {
            var ParameterViewModel = UnitOfViewModel.Instance.ParameterViewModel;

            Parameter itemSource = dbSource.Parameters.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            ParameterViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
