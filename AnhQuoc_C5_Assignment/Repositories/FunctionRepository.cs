using AnhQuoc_C5_Assignment;
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
    public class FunctionRepository : Repository<Function>
    {
        public FunctionRepository(): base()
        {
        }

        public FunctionRepository(ObservableCollection<Function> items): base(items)
        {
        }

    }
}
