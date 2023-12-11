using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class LoanHistoryViewModel : ViewModelBase<LoanHistory>
    {
        public LoanHistoryViewModel()
        {
            Item = new LoanHistory();
            Repo = new LoanHistoryRepository();
            prefix = Constants.prefixLoanHistory;
            numberPrefix = 2;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }
        
        public LoanHistory FindById(string idValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public LoanHistory FindById(ObservableCollection<LoanHistory> items, string idValue)
        {
            return items.FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public bool IsCheckEmptyItem(LoanHistoryDto item)
        {
            if (Utilities.IsCheckEmptyString(item.Id))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.IdReader))
            {
                return false;
            }

            if (item.Quantity == 0)
            {
                return false;
            }

            if (item.LoanDate == Constants.dateEmptyValue)
            {
                return false;
            }
            if (item.ExpDate == Constants.dateEmptyValue)
            {
                return false;
            }

            if (item.LoanPaid == 0)
            {
                return false;
            }

            if (item.Deposit == 0)
            {
                return false;
            }
            if (item.RemainPaid == 0)
            {
                return false;
            }

            if (item.FineMoney == 0)
            {
                return false;
            }
            if (item.Total == 0)
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.Note))
            {
                return false;
            }

            if (item.CreatedAt == Constants.dateEmptyValue)
            {
                return false;
            }
            return true;
        }


        public LoanHistory CreateByDto(LoanHistoryDto dto)
        {
            LoanHistory loanHistory = new LoanHistory();
            loanHistory.Id = dto.Id;
            Copy(loanHistory, dto);
            return loanHistory;
        }

        public void Copy(LoanHistory dest, LoanHistoryDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(LoanHistoryDto dest, LoanHistoryDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
