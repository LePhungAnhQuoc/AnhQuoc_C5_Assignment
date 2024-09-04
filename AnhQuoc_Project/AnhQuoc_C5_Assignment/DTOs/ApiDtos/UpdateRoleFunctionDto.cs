using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateRoleFunctionDto : IMap<RoleFunction>
    {
        public string IdRole { get; set; }

        public string IdFunction { get; set; }


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
