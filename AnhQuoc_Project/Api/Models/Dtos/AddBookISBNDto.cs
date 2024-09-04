using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddBookISBNDto : IMap<BookIsbn>
    {
        public string Isbn { get; set; } = null!;

        public string IdBookTitle { get; set; } = null!;

        public string IdAuthor { get; set; } = null!;

        public string OriginLanguage { get; set; } = null!;

        public string? Description { get; set; }

        public bool Status { get; set; }

        public AddBookISBNDto()
        {
            Status = true;
        }

        public void MapFrom(BookIsbn entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref BookIsbn entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
