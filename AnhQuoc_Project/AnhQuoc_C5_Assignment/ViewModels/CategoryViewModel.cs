using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public class CategoryViewModel : BaseViewModel<Category>
    {
        public CategoryViewModel()
        {
            Item = new Category();
            Repo = new CategoryRepository(new APIProvider<Category>(nameof(Category)));
            prefix = Constants.prefixCategory;
            numberPrefix = 2;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public Category FindById(string id)
        {
            foreach (Category item in Repo)
            {
                if (string.Compare(item.Id, id, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }


        public bool IsCheckEmptyItem(CategoryDto item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public Category CreateByDto(CategoryDto dto)
        {
            Category category = new Category();
            Copy(category, dto);
            return category;
        }

        public void Copy(Category dest, CategoryDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(CategoryDto dest, CategoryDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
