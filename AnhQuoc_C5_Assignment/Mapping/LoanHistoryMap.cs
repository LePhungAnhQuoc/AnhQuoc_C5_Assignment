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

            //newItem.IdReader = sourceItem.IdReader;
            //newItem.Quantity = sourceItem.Quantity;
            //newItem.LoanDate = sourceItem.LoanDate;
            //newItem.ExpDate = sourceItem.ExpDate;
            //newItem.LoanPaid = sourceItem.LoanPaid;
            //newItem.Deposit = sourceItem.Deposit;
            //newItem.RemainPaid = sourceItem.RemainPaid;
            //newItem.FineMoney = sourceItem.FineMoney;
            //newItem.Total = sourceItem.Total;
            //newItem.Note = sourceItem.Note;
            //newItem.CreatedAt = sourceItem.CreateAt;
            Utilities.Copy(newItem, sourceItem);
            
            return newItem;
        }
    }
}
