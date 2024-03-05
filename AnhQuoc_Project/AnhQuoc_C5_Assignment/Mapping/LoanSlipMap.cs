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
            var userVM = UnitOfViewModel.Instance.UserViewModel;
            var readerMap = UnitOfMap.Instance.ReaderMap;

            Reader reader = readerVM.FindById(sourceItem.IdReader);
            ReaderDto readerDto = readerMap.ConvertToDto(reader);
            User user = userVM.FindById(sourceItem.IdUser);
            LoanSlipDto newItem = new LoanSlipDto(sourceItem.Id);

            Utilities.Copy(newItem, sourceItem);
            newItem.Reader = reader;
            newItem.User = user;

            return newItem;
        }
    }
}
