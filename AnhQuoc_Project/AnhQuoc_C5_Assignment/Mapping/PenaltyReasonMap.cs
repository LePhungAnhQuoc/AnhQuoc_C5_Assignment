using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class PenaltyReasonMap : MapBase<PenaltyReason, PenaltyReasonDto>
    {
        public override PenaltyReasonDto ConvertToDto(PenaltyReason itemSource)
        {
            PenaltyReasonDto newItem = new PenaltyReasonDto(itemSource.Id);
            Utilitys.Copy(newItem, itemSource);
            return newItem;
        }
    }
}
