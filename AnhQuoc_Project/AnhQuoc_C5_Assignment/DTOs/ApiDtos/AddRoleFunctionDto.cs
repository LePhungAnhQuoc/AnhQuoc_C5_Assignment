using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddRoleFunctionDto : IMap<RoleFunction>
    {
        public string Id { get; set; }

        public string IdRole { get; set; }

        public string IdFunction { get; set; }


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
