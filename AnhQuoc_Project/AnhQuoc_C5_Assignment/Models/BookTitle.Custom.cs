using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class BookTitle : IMapFromModel
    {
        public object MapToAdd()
        {
            AddBookTitleDto result = new AddBookTitleDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateBookTitleDto result = new UpdateBookTitleDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
