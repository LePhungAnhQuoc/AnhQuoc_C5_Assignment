using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Reader : IMapFromModel
    {
        public object MapToAdd()
        {
            AddReaderDto result = new AddReaderDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateReaderDto result = new UpdateReaderDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
