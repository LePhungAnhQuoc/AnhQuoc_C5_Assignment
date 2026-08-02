using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public partial class LoanSlip : IMapFromModel
    {
        public object MapToAdd()
        {
            AddLoanSlipDto result = new AddLoanSlipDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateLoanSlipDto result = new UpdateLoanSlipDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
