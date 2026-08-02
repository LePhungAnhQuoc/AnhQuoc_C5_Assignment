using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Child : IMapFromModel
    {
        public object MapToAdd()
        {
            AddChildDto result = new AddChildDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateChildDto result = new UpdateChildDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
