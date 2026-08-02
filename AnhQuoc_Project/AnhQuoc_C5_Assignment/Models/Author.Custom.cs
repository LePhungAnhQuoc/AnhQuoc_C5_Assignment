using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Author : IMapFromModel
    {
        public object MapToAdd()
        {
            AddAuthorDto result = new AddAuthorDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateAuthorDto result = new UpdateAuthorDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
