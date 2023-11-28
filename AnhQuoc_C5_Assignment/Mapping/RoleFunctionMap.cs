using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class RoleFunctionMap : MapBase<RoleFunction, RoleFunctionDto>
    {
        public override RoleFunctionDto ConvertToDto(RoleFunction sourceItem)
        {
            var roleVM = UnitOfViewModel.Instance.RoleViewModel;
            var functionVM = UnitOfViewModel.Instance.FunctionViewModel;

            Role role = roleVM.FindById(sourceItem.IdRole);
            Function function = functionVM.FindById(sourceItem.IdFunction, null);

            RoleFunctionDto newItem = new RoleFunctionDto(sourceItem.Id);
            newItem.Role = role;
            newItem.Function = function;

            return newItem;
        }
    }
}
