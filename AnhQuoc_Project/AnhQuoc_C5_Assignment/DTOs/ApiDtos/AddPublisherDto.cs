using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddPublisherDto : IMap<Publisher>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }


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
