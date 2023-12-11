using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository() : base()
        {

        }
        public CategoryRepository(ObservableCollection<Category> items) : base(items)
        {
        }
        
     }
}
