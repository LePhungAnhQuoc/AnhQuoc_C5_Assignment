using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class PublisherMap : MapBase<Publisher, PublisherDto>
    {
        public override PublisherDto ConvertToDto(Publisher sourceItem)
        {
            PublisherDto newItem = new PublisherDto(sourceItem.Id);
            Utilitys.Copy(newItem, sourceItem);
            return newItem;
        }
    }
}
