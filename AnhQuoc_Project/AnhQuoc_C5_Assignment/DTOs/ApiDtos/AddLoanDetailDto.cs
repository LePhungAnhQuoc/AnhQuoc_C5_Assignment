using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddLoanDetailDto : IMap<LoanDetail>
    {
        public string Id { get; set; }

        public string IdLoan { get; set; }

        public int IdBook { get; set; }

        public DateTime ExpDate { get; set; }


        public AddLoanDetailDto()
        {
        }

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
