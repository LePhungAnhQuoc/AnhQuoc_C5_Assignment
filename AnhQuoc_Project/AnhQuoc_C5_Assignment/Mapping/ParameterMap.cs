using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ParameterMap : MapBase<Parameter, ParameterDto>
    {
        public override ParameterDto ConvertToDto(Parameter sourceItem)
        {
            ParameterDto newItem = new ParameterDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            return newItem;
        }
    }
}
