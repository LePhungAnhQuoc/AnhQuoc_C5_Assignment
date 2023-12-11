using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailMap : MapBase<LoanDetail, LoanDetailDto>
    {
        public override LoanDetailDto ConvertToDto(LoanDetail sourceItem)
        {
            var loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;

            LoanDetailDto newItem = new LoanDetailDto(sourceItem.Id);
            newItem.IdLoan = sourceItem.IdLoan;
            newItem.IdBook = sourceItem.IdBook;
            newItem.ExpDate = sourceItem.ExpDate;
            return newItem;
        }
    }
}
