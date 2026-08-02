using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Role : IMapFromModel
    {
        public object MapToAdd()
        {
            AddRoleDto result = new AddRoleDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateRoleDto result = new UpdateRoleDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
