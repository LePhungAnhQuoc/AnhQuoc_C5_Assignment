using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateParameterDto : IMap<Parameter>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Value { get; set; } = null!;

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
