using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddProvinceDto : IMap<Province>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool Status { get; set; }


        public AddProvinceDto()
        {
            Status = true;
        }

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
