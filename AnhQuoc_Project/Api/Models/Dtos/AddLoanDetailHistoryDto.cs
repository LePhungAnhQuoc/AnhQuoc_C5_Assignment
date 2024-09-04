using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddLoanDetailHistoryDto : IMap<LoanDetailHistory>
    {
        public string Id { get; set; } = null!;

        public string IdLoanHistory { get; set; } = null!;

        public int IdBook { get; set; }

        public DateTime ExpDate { get; set; }

        public decimal PaidMoney { get; set; }

        public string? Note { get; set; }


        public AddLoanDetailHistoryDto()
        {
            Note = string.Empty;
        }

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
