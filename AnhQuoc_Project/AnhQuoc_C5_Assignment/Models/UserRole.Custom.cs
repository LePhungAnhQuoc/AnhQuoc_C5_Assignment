using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class UserRole : IMapFromModel
    {
        public object MapToAdd()
        {
            AddUserRoleDto result = new AddUserRoleDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateUserRoleDto result = new UpdateUserRoleDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
