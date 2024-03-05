using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanHistoryMap : MapBase<LoanHistory, LoanHistoryDto>
    {
        public override LoanHistoryDto ConvertToDto(LoanHistory sourceItem)
        {
            ReaderViewModel readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            UserViewModel userVM = UnitOfViewModel.Instance.UserViewModel;

            LoanHistoryDto newItem = new LoanHistoryDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);


            newItem.User = userVM.FindById(sourceItem.IdUser);
            newItem.Reader = readerVM.FindById(sourceItem.IdReader);

            return newItem;
        }
    }
}
