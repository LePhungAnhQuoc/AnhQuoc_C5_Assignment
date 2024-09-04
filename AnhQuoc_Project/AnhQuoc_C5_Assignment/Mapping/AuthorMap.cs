using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class AuthorMap: MapBase<Author, AuthorDto>
    {
        public override AuthorDto ConvertToDto(Author sourceItem)
        {
            AuthorDto newItem = new AuthorDto(sourceItem.Id);
            Utilitys.Copy(newItem, sourceItem);
            
            return newItem;
        }
    }
}
