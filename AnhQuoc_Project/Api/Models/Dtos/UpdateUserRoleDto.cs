using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateUserRoleDto : IMap<UserRole>
    {
        public string IdUser { get; set; } = null!;

        public string IdRole { get; set; } = null!;


        public void MapFrom(UserRole entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref UserRole entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
