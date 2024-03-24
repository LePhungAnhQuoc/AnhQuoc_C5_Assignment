using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_C5_Assignment
{
    public class IdReaderToFullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            var readerMap = UnitOfMap.Instance.ReaderMap;

            Reader reader = readerVM.FindById(value.ToString());
            ReaderDto readerDto = readerMap.ConvertToDto(reader);

            return readerDto.FullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
