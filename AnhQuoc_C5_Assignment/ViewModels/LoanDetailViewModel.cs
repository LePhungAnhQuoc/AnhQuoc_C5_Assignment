using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailViewModel: ViewModelBase<LoanDetail>
    {
        public LoanDetailViewModel()
        {
            Item = new LoanDetail();
            Repo = new LoanDetailRepository();
            prefix = Constants.prefixLoanDetail;
        }


        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }


        public bool IsCheckEmptyItem(LoanDetail item)
        {
            return true;
        }
    }
}
