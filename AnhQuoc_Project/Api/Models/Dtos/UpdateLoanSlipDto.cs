using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateLoanSlipDto : IMap<LoanSlip>
    {
        public string IdUser { get; set; } = null!;

        public string IdReader { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime LoanDate { get; set; }

        public decimal LoanPaid { get; set; }

        public decimal Deposit { get; set; }


        public void MapFrom(LoanSlip entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref LoanSlip entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
