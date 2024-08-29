using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddUserDto : IMap<User>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Note { get; set; }

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
            Utilities.Copy(this, entity);
        }

        public void MapTo(ref User entity)
        {
            Utilities.Copy(entity, this);
        }
    }
}
