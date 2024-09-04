using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateBookStatusDto : IMap<BookStatu>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


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
