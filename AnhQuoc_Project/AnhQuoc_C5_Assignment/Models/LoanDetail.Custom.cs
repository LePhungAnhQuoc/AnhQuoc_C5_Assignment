using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class LoanDetail : IMapFromModel
    {
        public object MapToAdd()
        {
            AddLoanDetailDto result = new AddLoanDetailDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateLoanDetailDto result = new UpdateLoanDetailDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
