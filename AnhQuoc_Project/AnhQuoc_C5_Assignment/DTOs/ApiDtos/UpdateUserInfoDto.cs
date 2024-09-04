using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateUserInfoDto : IMap<UserInfo>
    {
        public string LName { get; set; }

        public string FName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }


        public void MapFrom(UserInfo entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref UserInfo entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
