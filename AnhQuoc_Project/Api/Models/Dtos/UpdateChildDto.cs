using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateChildDto : IMap<Child>
    {
        public string IdAdult { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }

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
