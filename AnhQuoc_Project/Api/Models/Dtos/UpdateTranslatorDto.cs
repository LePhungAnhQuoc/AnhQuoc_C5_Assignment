using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateTranslatorDto : IMap<Translator>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime BoF { get; set; }

        public string Summary { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


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
