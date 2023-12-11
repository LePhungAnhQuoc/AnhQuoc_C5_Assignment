using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ParameterViewModel : ViewModelBase<Parameter>
    {
        public ParameterViewModel()
        {
            Item = new Parameter();
            Repo = new ParameterRepository();
            prefix = Constants.prefixParameter;
            numberPrefix = 1;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public Parameter FindById(string id)
        {
            foreach (Parameter item in Repo)
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
