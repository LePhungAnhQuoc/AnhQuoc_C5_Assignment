using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class RoleFunctionViewModel: ViewModelBase<RoleFunction>
    {
        public RoleFunctionViewModel()
        {
            Item = new RoleFunction();
            Repo = new RoleFunctionRepository();
            prefix = Constants.prefixRoleFunction;
            numberPrefix = 1;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public RoleFunction FindById(string idValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public RoleFunction FindById(ObservableCollection<RoleFunction> items, string idValue)
        {
            return items.FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public bool IsCheckEmptyItem(RoleFunctionDto item)
        {
            if (item.Role == null)
            {
                return false;
            }
            if (item.Function == null)
            {
                return false;
            }

            return true;
        }


        public IEnumerable<RoleFunction> FillByIdRole(string idRole)
        {
            return Repo.Gets().Where(item => string.Compare(item.IdRole, idRole) == 0);
        }

        public RoleFunction FindByIdFunction(IEnumerable<RoleFunction> source, string idFunction)
        {
            return source.FirstOrDefault(item => string.Compare(item.IdFunction, idFunction, false) == 0);
        }

        public IEnumerable<RoleFunction> GetListFunctionByRole(string idRoleValue)
        {
            return Repo.Gets().Where(item => string.Compare(item.IdRole, idRoleValue, false) == 0);
        }
        

        public RoleFunction CreateByDto(RoleFunctionDto dto)
        {
            RoleFunction roleFunction = new RoleFunction();
            roleFunction.Id = dto.Id;
            Copy(roleFunction, dto);
            return roleFunction;
        }

        public ObservableCollection<RoleFunction> CreateByDto(ObservableCollection<RoleFunctionDto> dtos)
        {
            ObservableCollection<RoleFunction> result = new ObservableCollection<RoleFunction>();
            foreach (var dto in dtos)
            {
                RoleFunction roleFunction = CreateByDto(dto);
                result.Add(roleFunction);
            }
            return result;
        }

        public void Copy(RoleFunction dest, RoleFunctionDto source)
        {
            dest.IdRole = source.Role.Id;
            dest.IdFunction = source.Function.Id;
        }

        public void Copy(RoleFunctionDto dest, RoleFunctionDto source)
        {
            dest.Role = source.Role;
            dest.Function = source.Function;
        }
    }
}
