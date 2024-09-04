using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateRoleDto : IMap<Role>
    {
        public string Name { get; set; }

        public string Group { get; set; }

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
