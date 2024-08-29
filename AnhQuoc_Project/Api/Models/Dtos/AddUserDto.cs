using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddUserDto : IMap<User>
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Note { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public AddUserDto()
        {
            Note = "";
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

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
