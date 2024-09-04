using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateProvinceDto : IMap<Province>
    {
        public string Name { get; set; } = null!;

        public bool Status { get; set; }


        public void MapFrom(Province entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Province entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
