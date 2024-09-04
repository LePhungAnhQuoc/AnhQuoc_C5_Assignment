using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddPenaltyReasonDto : IMap<PenaltyReason>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddPenaltyReasonDto()
        {
            CreatedAt = ModifiedAt = DateTime.Now;
        }

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
