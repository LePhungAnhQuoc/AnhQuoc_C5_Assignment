using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Publisher : IMapFromModel
    {
        public object MapToAdd()
        {
            AddPublisherDto result = new AddPublisherDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdatePublisherDto result = new UpdatePublisherDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
