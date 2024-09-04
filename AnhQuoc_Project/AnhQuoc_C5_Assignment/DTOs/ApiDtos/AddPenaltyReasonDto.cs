using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddPenaltyReasonDto : IMap<PenaltyReason>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

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
