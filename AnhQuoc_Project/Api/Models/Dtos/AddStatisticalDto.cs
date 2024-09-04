using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddStatisticalDto : IMap<Statistical>
    {
        public DateTime DateTime { get; set; }

        public string Data { get; set; } = null!;

        public string Description { get; set; } = null!;


        public AddStatisticalDto()
        {
        }

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
