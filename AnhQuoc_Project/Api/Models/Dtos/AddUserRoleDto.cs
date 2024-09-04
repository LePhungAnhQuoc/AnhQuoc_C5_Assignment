using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddUserRoleDto : IMap<UserRole>
    {
        public string Id { get; set; } = null!;

        public string IdUser { get; set; } = null!;

        public string IdRole { get; set; } = null!;


        public AddUserRoleDto()
        {
        }

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
