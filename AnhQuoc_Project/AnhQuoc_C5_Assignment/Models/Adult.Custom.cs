using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Adult : IMapFromModel
    {
        public object MapToAdd()
        {
            AddAdultDto result = new AddAdultDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateAdultDto result = new UpdateAdultDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
