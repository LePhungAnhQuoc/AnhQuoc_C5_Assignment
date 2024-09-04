using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddParameterDto : IMap<Parameter>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddParameterDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

        public void MapFrom(Parameter entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Parameter entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
