using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailViewModel: ViewModelBase<LoanDetail>
    {
        public LoanDetailViewModel()
        {
            Item = new LoanDetail();
            Repo = new LoanDetailRepository();
            prefix = Constants.prefixLoanDetail;
            numberPrefix = 2;
        }


        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public ObservableCollection<LoanDetail> FillListByISBN(string isbn, bool? bookStatusValue)
        {
            return FillListByISBN(Repo.Gets(), isbn, bookStatusValue);
        }

        public ObservableCollection<LoanDetail> FillListByISBN(ObservableCollection<LoanDetail> source, string isbn, bool? bookStatusValue)
        {
            var bookVM = UnitOfViewModel.Instance.BookViewModel;

            ObservableCollection<LoanDetail> newList = new ObservableCollection<LoanDetail>();
            foreach (LoanDetail item in source)
            {
                Book book = bookVM.FindById(item.IdBook, bookStatusValue);
                
                if (string.Compare(book.ISBN, isbn, false) == 0)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

        public ObservableCollection<LoanDetail> FillListByIdLoan(string idLoan)
        {
            return FillListByIdLoan(Repo.Gets(), idLoan);
        }

        public ObservableCollection<LoanDetail> FillListByIdLoan(ObservableCollection<LoanDetail> source, string idLoan)
        {
            ObservableCollection<LoanDetail> newList = new ObservableCollection<LoanDetail>();
            foreach (LoanDetail item in source)
            {
                if (string.Compare(item.IdLoan, idLoan, false) == 0)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }
    }
}
