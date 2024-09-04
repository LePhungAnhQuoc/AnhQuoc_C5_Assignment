using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddLoanDetailDto : IMap<LoanDetail>
    {
        public string Id { get; set; } = null!;

        public string IdLoan { get; set; } = null!;

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
