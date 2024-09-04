using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddRoleDto : IMap<Role>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Group { get; set; }

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
