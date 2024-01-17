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
            Repo = new CategoryRepository();
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
    }
}
