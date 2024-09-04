using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateTranslatorDto : IMap<Translator>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime boF { get; set; }

        public string Summary { get; set; }

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
