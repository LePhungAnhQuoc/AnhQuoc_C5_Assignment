using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddPublisherDto : IMap<Publisher>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;


        public AddPublisherDto()
        {
        }

        public void MapFrom(Publisher entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Publisher entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
