using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateLoanDetailHistoryDto : IMap<LoanDetailHistory>
    {
        public string IdLoanHistory { get; set; } = null!;

        public int IdBook { get; set; }

        public decimal PaidMoney { get; set; }

        public string? Note { get; set; }


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
