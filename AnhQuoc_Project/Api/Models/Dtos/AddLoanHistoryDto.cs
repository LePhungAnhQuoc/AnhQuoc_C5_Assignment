using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddLoanHistoryDto : IMap<LoanHistory>
    {
        public string Id { get; set; } = null!;

        public string IdUser { get; set; } = null!;

        public string IdReader { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime ExpDate { get; set; }

        public decimal LoanPaid { get; set; }

        public decimal Deposit { get; set; }

        public decimal FineMoney { get; set; }

        public decimal Total { get; set; }

        public string? Note { get; set; }

        public DateTime CreateAt { get; set; }


        public AddLoanHistoryDto()
        {
            CreateAt = DateTime.Now;
        }

        public void MapFrom(LoanHistory entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref LoanHistory entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
