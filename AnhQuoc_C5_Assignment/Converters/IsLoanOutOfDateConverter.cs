using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    class IsLoanOutOfDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var loanVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            LoanDetail loanDetail = parameter as LoanDetail;
            LoanSlip loan = loanVM.FindById(loanDetail.IdLoan);

            DateTime expDate = (DateTime)value;

            return loanDetail.ExpDate.Date > DateTime.Now.Date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
