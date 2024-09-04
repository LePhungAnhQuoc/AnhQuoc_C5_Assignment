using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdatePublisherDto : IMap<Publisher>
    {
        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;


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
