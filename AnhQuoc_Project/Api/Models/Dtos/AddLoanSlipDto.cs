using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddLoanSlipDto : IMap<LoanSlip>
    {
        public string Id { get; set; } = null!;

        public string IdUser { get; set; } = null!;

        public string IdReader { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime ExpDate { get; set; }

        public decimal LoanPaid { get; set; }

        public decimal Deposit { get; set; }


        public AddLoanSlipDto()
        {
        }

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
