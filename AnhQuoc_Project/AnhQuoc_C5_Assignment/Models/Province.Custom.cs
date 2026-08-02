using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Province : IMapFromModel
    {
        public object MapToAdd()
        {
            AddProvinceDto result = new AddProvinceDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateProvinceDto result = new UpdateProvinceDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
