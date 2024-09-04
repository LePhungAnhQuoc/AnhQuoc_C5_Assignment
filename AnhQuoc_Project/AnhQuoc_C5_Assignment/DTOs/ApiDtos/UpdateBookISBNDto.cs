using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateBookISBNDto : IMap<BookISBN>
    {
        public string IdBookTitle { get; set; }

        public string IdAuthor { get; set; }

        public string OriginLanguage { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }


        public void MapFrom(BookISBN entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref BookISBN entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
