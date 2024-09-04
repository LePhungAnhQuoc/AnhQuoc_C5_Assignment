using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddParameterDto : IMap<Parameter>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Value { get; set; } = null!;

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
