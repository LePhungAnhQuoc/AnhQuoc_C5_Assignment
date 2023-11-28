using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public interface IMap<T, TDto>
    {
        TDto ConvertToDto(T itemSource);
        ObservableCollection<TDto> ConvertToDto(ObservableCollection<T> sourceList);
    }
}
