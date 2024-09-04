using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateUserInfoDto : IMap<UserInfo>
    {
        public string Lname { get; set; } = null!;

        public string Fname { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;


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
