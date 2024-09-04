using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ParameterViewModel : BaseViewModel<Parameter>
    {
        public ParameterViewModel()
        {
            Item = new Parameter();
            Repo = new ParameterRepository(new APIProvider<Parameter>(nameof(Parameter)));
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


        public bool IsCheckEmptyItem(ParameterDto item)
        {
            return Utilitys.IsCheckEmptyItem(item, false);
        }

        public Parameter CreateByDto(ParameterDto dto)
        {
            Parameter parameter = new Parameter();
            Copy(parameter, dto);
            return parameter;
        }

        public void Copy(Parameter dest, ParameterDto source)
        {
            Utilitys.Copy(dest, source);
        }

        public void Copy(ParameterDto dest, ParameterDto source)
        {
            Utilitys.Copy(dest, source);
        }
    }
}
