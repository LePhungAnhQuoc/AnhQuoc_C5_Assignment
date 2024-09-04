using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddUserInfoDto : IMap<UserInfo>
    {
        public string IdUser { get; set; }

        public string LName { get; set; }

        public string FName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }


        public AddUserInfoDto()
        {
        }

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
