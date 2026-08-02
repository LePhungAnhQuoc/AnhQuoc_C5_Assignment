using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Parameter : IMapFromModel
    {
        public object MapToAdd()
        {
            AddParameterDto result = new AddParameterDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateParameterDto result = new UpdateParameterDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
