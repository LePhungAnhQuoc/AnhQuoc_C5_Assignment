using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailViewModel: BaseViewModel<LoanDetail>
    {
        public LoanDetailViewModel()
        {
            Item = new LoanDetail();
            Repo = new LoanDetailRepository(new APIProvider<LoanDetail>(nameof(LoanDetail)));
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

        public ObservableCollection<LoanDetail> FillListByIdLoans(ObservableCollection<LoanSlip> loans)
        {
            var source = Repo.Gets();
            ObservableCollection<LoanDetail> result = new ObservableCollection<LoanDetail>();

            foreach (LoanSlip loan in loans)
            {
                result.AddRange(FillListByIdLoan(source, loan.Id));
            }
            return result;
        }


        public bool IsOutOfExpireDate(ObservableCollection<LoanDetail> details)
        {
            return details.FirstOrDefault((item) => IsOutOfExpireDate(item)) != null;
        }

        public bool IsOutOfExpireDate(LoanDetail detail)
        {
            return DateTime.Now > detail.ExpDate;
        }


        public LoanDetail CreateByDto(LoanDetailDto dto)
        {
            LoanDetail loanDetail = new LoanDetail();
            Copy(loanDetail, dto);
            return loanDetail;
        }

        public void Copy(LoanDetail dest, LoanDetailDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(LoanDetailDto dest, LoanDetailDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
