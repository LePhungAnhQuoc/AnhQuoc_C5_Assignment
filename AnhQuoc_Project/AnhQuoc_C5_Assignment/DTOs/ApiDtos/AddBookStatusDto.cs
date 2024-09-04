using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddBookStatusDto : IMap<BookStatu>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddBookStatusDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

        public void MapFrom(BookStatu entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref BookStatu entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
