using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class Book : IMapFromModel
    {
        public object MapToAdd()
        {
            AddBookDto result = new AddBookDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateBookDto result = new UpdateBookDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
