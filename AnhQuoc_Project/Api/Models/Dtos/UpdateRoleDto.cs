using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateRoleDto : IMap<Role>
    {
        public string Name { get; set; } = null!;

        public string Group { get; set; } = null!;

        public bool Status { get; set; }


        public void MapFrom(Role entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Role entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
