using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddAdultDto : IMap<Adult>
    {
        public string IdReader { get; set; } = null!;

        public string? Identify { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime ExpireDate { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public AddAdultDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

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
