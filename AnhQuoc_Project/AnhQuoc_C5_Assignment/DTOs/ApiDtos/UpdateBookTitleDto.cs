using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateBookTitleDto : IMap<BookTitle>
    {
        public string IdCategory { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public string Summary { get; set; }

        public string UrlImage { get; set; }


        public void MapFrom(BookTitle entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref BookTitle entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
