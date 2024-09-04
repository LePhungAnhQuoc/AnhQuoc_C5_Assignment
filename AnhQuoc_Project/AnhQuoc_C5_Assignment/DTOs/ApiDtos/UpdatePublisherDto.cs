using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdatePublisherDto : IMap<Publisher>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }


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
