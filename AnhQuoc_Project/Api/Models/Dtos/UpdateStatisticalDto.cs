using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateStatisticalDto : IMap<Statistical>
    {
        public string Data { get; set; } = null!;

        public string Description { get; set; } = null!;


        public void MapFrom(Statistical entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Statistical entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
