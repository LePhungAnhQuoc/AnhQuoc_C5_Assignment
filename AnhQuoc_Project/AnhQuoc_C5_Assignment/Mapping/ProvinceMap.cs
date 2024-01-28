using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ProvinceMap: MapBase<Province, ProvinceDto>
    {
        public override ProvinceDto ConvertToDto(Province sourceItem)
        {
            ProvinceDto newItem = new ProvinceDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            return newItem;
        }
    }
}
