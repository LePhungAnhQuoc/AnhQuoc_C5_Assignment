using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateLoanSlipDto : IMap<LoanSlip>
    {
        public string IdUser { get; set; }

        public string IdReader { get; set; }

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
