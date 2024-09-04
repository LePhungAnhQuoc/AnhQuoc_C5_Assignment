using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateRoleFunctionDto : IMap<RoleFunction>
    {
        public string IdRole { get; set; } = null!;

        public string IdFunction { get; set; } = null!;


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
