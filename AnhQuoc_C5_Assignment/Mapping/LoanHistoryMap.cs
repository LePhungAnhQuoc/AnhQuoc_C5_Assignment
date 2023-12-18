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
            LoanHistoryDto newItem = new LoanHistoryDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);
            
            return newItem;
        }
    }
}
