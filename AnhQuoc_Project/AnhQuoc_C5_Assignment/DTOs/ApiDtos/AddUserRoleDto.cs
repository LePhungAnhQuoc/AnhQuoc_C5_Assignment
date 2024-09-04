using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddUserRoleDto : IMap<UserRole>
    {
        public string Id { get; set; }

        public string IdUser { get; set; }

        public string IdRole { get; set; }


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
