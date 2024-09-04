using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddBookTitleDto : IMap<BookTitle>
    {
        public string Id { get; set; }

        public string IdCategory { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public string Summary { get; set; }

        public string UrlImage { get; set; }


        public AddBookTitleDto()
        {
        }

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
