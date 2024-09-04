using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddBookDto : IMap<Book>
    {
        public int Id { get; set; }

        public string Isbn { get; set; } = null!;

        public string IdPublisher { get; set; } = null!;

        public string IdTranslator { get; set; } = null!;

        public string Language { get; set; } = null!;

        public DateTime PublishDate { get; set; }

        public decimal Price { get; set; }

        public decimal PriceCurrent { get; set; }

        public string? Note { get; set; }

        public string IdBookStatus { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddBookDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

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
