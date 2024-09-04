using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddRoleDto : IMap<Role>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Group { get; set; } = null!;

        public bool Status { get; set; }


        public AddRoleDto()
        {
            Status = true;
        }

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
