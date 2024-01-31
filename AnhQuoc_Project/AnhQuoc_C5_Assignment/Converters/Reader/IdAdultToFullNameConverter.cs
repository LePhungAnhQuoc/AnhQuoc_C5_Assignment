using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    public class IdAdultToFullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var adultVM = UnitOfViewModel.Instance.AdultViewModel;
            var readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            var readerMap = UnitOfMap.Instance.ReaderMap;

            Adult adult = adultVM.FindByIdReader(value.ToString(), null);
            Reader reader = readerVM.FindById(adult.IdReader);
            ReaderDto readerDto = readerMap.ConvertToDto(reader);

            return readerDto.FullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
