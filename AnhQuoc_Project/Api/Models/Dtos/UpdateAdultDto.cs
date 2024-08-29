using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateAdultDto : IMap<Adult>
    {
        public string? Identify { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime ExpireDate { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(Adult entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Adult entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
