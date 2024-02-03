using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    public class BorrowBookSelectReaderButtonConfirmConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Some validates
            if (values.Length < 2)
                return false;

            // Code here
            if (values[0] == null)
                return false;
            if (values[1] == null)
                return false;

            var loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;
            bool isOutOfExpire = loanDetailVM.IsOutOfExpireDate(values[1] as ObservableCollection<LoanDetail>);

            if (isOutOfExpire)
                return false;

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
