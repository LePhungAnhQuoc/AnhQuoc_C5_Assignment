using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanSlipMap : MapBase<LoanSlip, LoanSlipDto>
    {
        public override LoanSlipDto ConvertToDto(LoanSlip sourceItem)
        {
            var readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            var readerMap = UnitOfMap.Instance.ReaderMap;

            Reader reader = readerVM.FindById(sourceItem.IdReader);
            ReaderDto readerDto = readerMap.ConvertToDto(reader);

            LoanSlipDto newItem = new LoanSlipDto(sourceItem.Id);
            newItem.ReaderName = readerDto.FullName;
            newItem.Quantity = sourceItem.Quantity;
            
            newItem.LoanDate = sourceItem.LoanDate;
            newItem.ExpDate = sourceItem.ExpDate;

            newItem.LoanPaid = sourceItem.LoanPaid;
            newItem.Deposit = sourceItem.Deposit;

            return newItem;
        }
    }
}
