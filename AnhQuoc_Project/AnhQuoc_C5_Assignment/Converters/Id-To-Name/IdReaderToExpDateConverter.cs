using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    public class IdReaderToExpDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            var adultVM = UnitOfViewModel.Instance.AdultViewModel;
            var readerMap = UnitOfMap.Instance.ReaderMap;


            Reader reader = readerVM.FindById(value.ToString());
            Adult adult = adultVM.FindByIdReader(reader.Id);

            if (adult == null)
                return string.Empty;
            return adult.ExpireDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
            throw new NotImplementedException();
        }
    }
}
