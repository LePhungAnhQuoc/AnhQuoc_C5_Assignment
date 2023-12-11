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
    public class LoanHistoryRepository : Repository<LoanHistory>
    {
        public LoanHistoryRepository(): base()
        {
        }

        public LoanHistoryRepository(ObservableCollection<LoanHistory> items): base(items)
        {
        }
        
        public override void WriteUpdate(LoanHistory updated)
        {
            var LoanHistoryViewModel = UnitOfViewModel.Instance.LoanHistoryViewModel;

            LoanHistory itemSource = dbSource.LoanHistories.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            LoanHistoryViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
