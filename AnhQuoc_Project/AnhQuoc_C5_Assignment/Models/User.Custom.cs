using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class User : IMapFromModel
    {
        public object MapToAdd()
        {
            AddUserDto result = new AddUserDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateUserDto result = new UpdateUserDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
