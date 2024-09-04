using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddRoleFunctionDto : IMap<RoleFunction>
    {
        public string Id { get; set; } = null!;

        public string IdRole { get; set; } = null!;

        public string IdFunction { get; set; } = null!;


        public AddRoleFunctionDto()
        {
        }

        public void MapFrom(RoleFunction entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref RoleFunction entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
