using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Translator : IMapFromModel
    {
        public object MapToAdd()
        {
            AddTranslatorDto result = new AddTranslatorDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateTranslatorDto result = new UpdateTranslatorDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
