using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddBookISBNDto : IMap<BookISBN>
    {
        public string ISBN { get; set; }

        public string IdBookTitle { get; set; }

        public string IdAuthor { get; set; }

        public string OriginLanguage { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public AddBookISBNDto()
        {
            Status = true;
        }

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
