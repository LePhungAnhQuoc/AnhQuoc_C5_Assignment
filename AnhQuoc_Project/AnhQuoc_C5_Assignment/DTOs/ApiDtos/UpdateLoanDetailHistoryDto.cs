using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateLoanDetailHistoryDto : IMap<LoanDetailHistory>
    {
        public string IdLoanHistory { get; set; }

        public int IdBook { get; set; }

        public decimal PaidMoney { get; set; }

        public string Note { get; set; }


        public void MapFrom(LoanDetailHistory entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref LoanDetailHistory entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
