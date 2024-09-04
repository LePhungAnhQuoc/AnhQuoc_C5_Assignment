using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateLoanHistoryDto : IMap<LoanHistory>
    {
        public string IdUser { get; set; }

        public string IdReader { get; set; }

        public int Quantity { get; set; }

        public DateTime LoanDate { get; set; }

        public decimal LoanPaid { get; set; }

        public decimal Deposit { get; set; }

        public decimal FineMoney { get; set; }

        public decimal Total { get; set; }

        public string Note { get; set; }


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
