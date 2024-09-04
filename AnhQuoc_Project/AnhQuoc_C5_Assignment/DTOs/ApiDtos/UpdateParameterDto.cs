using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateParameterDto : IMap<Parameter>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


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
