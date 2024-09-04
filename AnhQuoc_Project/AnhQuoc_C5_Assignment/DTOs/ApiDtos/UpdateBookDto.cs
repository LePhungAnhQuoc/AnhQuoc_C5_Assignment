using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateBookDto : IMap<Book>
    {
        public string ISBN { get; set; }

        public string IdPublisher { get; set; }

        public string IdTranslator { get; set; }

        public string Language { get; set; }

        public DateTime PublishDate { get; set; }

        public decimal Price { get; set; }

        public decimal PriceCurrent { get; set; }

        public string Note { get; set; }

        public string IdBookStatus { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(Book entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Book entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
