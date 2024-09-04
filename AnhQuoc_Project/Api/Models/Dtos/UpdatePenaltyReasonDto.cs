using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdatePenaltyReasonDto : IMap<PenaltyReason>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(PenaltyReason entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref PenaltyReason entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
