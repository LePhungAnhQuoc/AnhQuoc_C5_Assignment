using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class RoleMap : MapBase<Role, RoleDto>
    {
        public override RoleDto ConvertToDto(Role sourceItem)
        {
            var roleVM = UnitOfViewModel.Instance.RoleViewModel;

            RoleDto newItem = new RoleDto(sourceItem.Id);
            Utilitys.Copy(newItem, sourceItem);
            return newItem;
        }
    }
}
