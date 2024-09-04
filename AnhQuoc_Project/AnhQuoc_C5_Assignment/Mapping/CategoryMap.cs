using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class CategoryMap : MapBase<Category, CategoryDto>
    {
        public override CategoryDto ConvertToDto(Category sourceItem)
        {
            CategoryDto newItem = new CategoryDto(sourceItem.Id);
            Utilitys.Copy(newItem, sourceItem);
            return newItem;
        }
    }
}
