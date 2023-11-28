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
            newItem.Name = sourceItem.Name;
            newItem.Description = sourceItem.Description;
            newItem.boF = sourceItem.boF;
            newItem.Summary = sourceItem.Summary;
            newItem.Status = sourceItem.Status;
            newItem.CreatedAt = sourceItem.CreatedAt;
            newItem.ModifiedAt = sourceItem.ModifiedAt;

            return newItem;
        }
    }
}
