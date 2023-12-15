using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailHistoryViewModel: ViewModelBase<LoanDetailHistory>
    {
        public LoanDetailHistoryViewModel()
        {
            Item = new LoanDetailHistory();
            Repo = new LoanDetailHistoryRepository();
            prefix = Constants.prefixLoanDetailHistory;
            numberPrefix = 2;
        }


        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public ObservableCollection<LoanDetailHistory> FillListByISBN(string isbn, bool? bookStatusValue)
        {
            return FillListByISBN(Repo.Gets(), isbn, bookStatusValue);
        }

        public ObservableCollection<LoanDetailHistory> FillListByISBN(ObservableCollection<LoanDetailHistory> source, string isbn, bool? bookStatusValue)
        {
            var bookVM = UnitOfViewModel.Instance.BookViewModel;

            ObservableCollection<LoanDetailHistory> newList = new ObservableCollection<LoanDetailHistory>();
            foreach (LoanDetailHistory item in source)
            {
                Book book = bookVM.FindById(item.IdBook, bookStatusValue);
                
                if (string.Compare(book.ISBN, isbn, false) == 0)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

        public ObservableCollection<LoanDetailHistory> FillListByIdLoanHistory(string idLoanHistory)
        {
            return FillListByIdLoanHistory(Repo.Gets(), idLoanHistory);
        }

        public ObservableCollection<LoanDetailHistory> FillListByIdLoanHistory(ObservableCollection<LoanDetailHistory> source, string idLoanHistory)
        {
            ObservableCollection<LoanDetailHistory> newList = new ObservableCollection<LoanDetailHistory>();
            foreach (LoanDetailHistory item in source)
            {
                if (string.Compare(item.IdLoanHistory, idLoanHistory, false) == 0)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

        public bool IsCheckEmptyItem(LoanDetailHistoryDto item, bool isExceptProperty, params string[] checkProperties)
        {
            return Utilities.IsCheckEmptyItem(item, isExceptProperty, checkProperties);
        }

        public LoanDetailHistory CreateByDto(LoanDetailHistoryDto dto)
        {
            LoanDetailHistory LoanDetailHistory = new LoanDetailHistory();
            Copy(LoanDetailHistory, dto);
            return LoanDetailHistory;
        }

        public void Copy(LoanDetailHistory dest, LoanDetailHistoryDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(LoanDetailHistoryDto dest, LoanDetailHistoryDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
