using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Function : IMapFromModel
    {
        public object MapToAdd()
        {
            AddFunctionDto result = new AddFunctionDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateFunctionDto result = new UpdateFunctionDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
