using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateUserDto : IMap<User>
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Note { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(User entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref User entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
