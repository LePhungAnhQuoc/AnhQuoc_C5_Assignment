using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnhQuoc_C5_Assignment.Models;

namespace AnhQuoc_C5_Assignment.DTOs.ApiDtos
{
    public class MapFromModel<TAddDto, TUpdateDto> where TAddDto : class, new() where TUpdateDto : class, new()
    {
        public object MapToAdd()
        {
            TAddDto result = new TAddDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            TUpdateDto result = new TUpdateDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
