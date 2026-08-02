using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class BookStatu : IMapFromModel
    {
        public object MapToAdd()
        {
            AddBookStatusDto result = new AddBookStatusDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateBookStatusDto result = new UpdateBookStatusDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
