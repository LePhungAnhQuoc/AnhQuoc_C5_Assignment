using AnhQuoc_C5_Assignment;
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
    public class FunctionRepository : Repository<Function>
    {
        public FunctionRepository(): base()
        {
        }

        public FunctionRepository(ObservableCollection<Function> items): base(items)
        {
        }

        public override void WriteUpdate(Function updated)
        {
            var FunctionViewModel = UnitOfViewModel.Instance.FunctionViewModel;

            Function itemSource = dbSource.Functions.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            FunctionViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Function item, bool status)
        {
            Function itemSource = dbSource.Functions.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
