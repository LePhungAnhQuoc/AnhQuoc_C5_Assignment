using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class LoanDetailHistory : IMapFromModel
    {
        public object MapToAdd()
        {
            AddLoanDetailHistoryDto result = new AddLoanDetailHistoryDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateLoanDetailHistoryDto result = new UpdateLoanDetailHistoryDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
