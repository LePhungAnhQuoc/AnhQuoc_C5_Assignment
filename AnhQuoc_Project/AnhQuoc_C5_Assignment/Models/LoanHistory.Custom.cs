using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class LoanHistory : IMapFromModel
    {
        public object MapToAdd()
        {
            AddLoanHistoryDto result = new AddLoanHistoryDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateLoanHistoryDto result = new UpdateLoanHistoryDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
