using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    class IsLoanOutOfDateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var loanVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            LoanDetail loanDetail = values[0] as LoanDetail;
            LoanSlip loan = loanVM.FindById(loanDetail.IdLoan);

            if (loanDetail.ExpDate.Date > DateTime.Now.Date)
                return Brushes.Red;
            return Brushes.Black;
        }
        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
