using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Category : IMapFromModel
    {
        public object MapToAdd()
        {
            AddCategoryDto result = new AddCategoryDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateCategoryDto result = new UpdateCategoryDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
