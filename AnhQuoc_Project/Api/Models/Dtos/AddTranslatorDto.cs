using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddTranslatorDto : IMap<Translator>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime BoF { get; set; }

        public string Summary { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddTranslatorDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

        public void MapFrom(Translator entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Translator entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
