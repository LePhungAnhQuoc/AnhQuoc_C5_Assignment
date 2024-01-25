using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public abstract class MapBase<T, TDto> : IMap<T, TDto>
    {
        public abstract TDto ConvertToDto(T itemSource);

        public virtual ObservableCollection<TDto> ConvertToDto(ObservableCollection<T> sourceList)
        {
            ObservableCollection<TDto> result = new ObservableCollection<TDto>();

            foreach (T itemSource in sourceList)
            {
                TDto newItem = ConvertToDto(itemSource);
                result.Add(newItem);
            }
            return result;
        }
    }
}
