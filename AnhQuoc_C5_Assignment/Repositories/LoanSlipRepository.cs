using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanSlipRepository : Repository<LoanSlip>
    {
        public LoanSlipRepository() : base()
        {
        }
        public LoanSlipRepository(ObservableCollection<LoanSlip> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.LoanSlips.ToObservableCollection();
        }

        public override void WriteAdd(LoanSlip item)
        {
            dbSource.LoanSlips.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(LoanSlip item)
        {
            dbSource.LoanSlips.Remove(item);

            dbSource.SaveChanges();
        }

        public override void WriteUpdate(LoanSlip updated)
        {
            var LoanSlipViewModel = UnitOfViewModel.Instance.LoanSlipViewModel;

            LoanSlip itemSource = dbSource.LoanSlips.FirstOrDefault(sourceItem => sourceItem.IdReader == updated.IdReader);
            LoanSlipViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
