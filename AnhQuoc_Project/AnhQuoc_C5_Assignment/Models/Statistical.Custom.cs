using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Statistical : IMapFromModel
    {
        public object MapToAdd()
        {
            AddStatisticalDto result = new AddStatisticalDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateStatisticalDto result = new UpdateStatisticalDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
