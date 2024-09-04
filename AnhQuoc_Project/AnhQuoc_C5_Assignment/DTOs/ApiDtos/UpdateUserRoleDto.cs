using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateUserRoleDto : IMap<UserRole>
    {
        public string IdUser { get; set; }

        public string IdRole { get; set; }


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
