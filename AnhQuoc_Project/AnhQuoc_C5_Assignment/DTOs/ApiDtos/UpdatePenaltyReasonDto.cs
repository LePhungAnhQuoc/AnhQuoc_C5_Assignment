using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdatePenaltyReasonDto : IMap<PenaltyReason>
    {
        public string Name { get; set; }

        public string Description { get; set; }

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
