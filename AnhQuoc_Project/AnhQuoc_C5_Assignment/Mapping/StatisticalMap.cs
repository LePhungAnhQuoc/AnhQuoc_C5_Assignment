using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class StatisticalMap : MapBase<Statistical, StatisticalDto>
    {
        public override StatisticalDto ConvertToDto(Statistical sourceItem)
        {
            StatisticalDto newItem = new StatisticalDto(sourceItem.DateTime);
            Utilitys.Copy(newItem, sourceItem);

            return newItem;
        }
    }
}
