using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanSlipViewModel: ViewModelBase<LoanSlip>
    {
        public LoanSlipViewModel()
        {
            Item = new LoanSlip();
            Repo = new LoanSlipRepository();
            prefix = Constants.prefixLoan;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public ObservableCollection<LoanSlip> FillByIdReader(string idReader, bool ignoreCase = false)
        {
            return FillByIdReader(Repo.Gets(), idReader, ignoreCase);
        }

        public ObservableCollection<LoanSlip> FillByIdReader(ObservableCollection<LoanSlip> source, string idReader, bool ignoreCase = false)
        {
            return source.Where(item => string.Compare(item.IdReader , idReader, ignoreCase) == 0).ToObservableCollection();
        }
    }
}
