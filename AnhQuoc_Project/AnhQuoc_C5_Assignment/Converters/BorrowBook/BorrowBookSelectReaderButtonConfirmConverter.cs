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
            if (values[2] == null)
                return false;
            
            var reader = values[0] as ReaderDto;

            LoanDetailViewModel loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;
            ReaderViewModel readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            AdultViewModel adultVM = UnitOfViewModel.Instance.AdultViewModel;
            ChildViewModel childVM = UnitOfViewModel.Instance.ChildViewModel;

            bool isOutOfExpireLoanDetail = loanDetailVM.IsOutOfExpireDate(values[1] as ObservableCollection<LoanDetail>);

            ObservableCollection<Book> booksOfReader = values[2] as ObservableCollection<Book>;

            #region Parameter
            ParameterViewModel paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            Parameter para = paraVM.FindById(Constants.paraQD2);
            int value = -1;
            int.TryParse(para.Value, out value);
            #endregion

            Reader adultReaderFind = null;
            if (reader.ReaderType == ReaderType.Adult)
                adultReaderFind = readerVM.FindById(reader.Id);
            else if (reader.ReaderType == ReaderType.Child)
            {
                Child child = childVM.FindByIdReader(reader.Id);
                Adult adult = adultVM.FindByIdReader(child.IdAdult);
                adultReaderFind = readerVM.FindById(adult.IdReader);
            }

            if (adultReaderFind.Status == false || isOutOfExpireLoanDetail || booksOfReader.Count >= value)
                return false;

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
