using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddChildDto : IMap<Child>
    {
        public string IdReader { get; set; } = null!;

        public string IdAdult { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public AddChildDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

        public void MapFrom(Child entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Child entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
