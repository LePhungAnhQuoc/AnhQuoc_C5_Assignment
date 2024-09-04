using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateLoanDetailDto : IMap<LoanDetail>
    {
        public string IdLoan { get; set; }

        public int IdBook { get; set; }


        public void MapFrom(LoanDetail entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref LoanDetail entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
