using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class RoleFunction : IMapFromModel
    {
        public object MapToAdd()
        {
            AddRoleFunctionDto result = new AddRoleFunctionDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateRoleFunctionDto result = new UpdateRoleFunctionDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
