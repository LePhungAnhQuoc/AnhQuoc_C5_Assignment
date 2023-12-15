using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailHistoryMap : MapBase<LoanDetailHistory, LoanDetailHistoryDto>
    {
        public override LoanDetailHistoryDto ConvertToDto(LoanDetailHistory sourceItem)
        {
            LoanDetailHistoryDto newItem = new LoanDetailHistoryDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);
            
            return newItem;
        }
    }
}
