using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddBookTitleDto : IMap<BookTitle>
    {
        public string Id { get; set; } = null!;

        public string IdCategory { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Note { get; set; }

        public string Summary { get; set; } = null!;

        public string UrlImage { get; set; } = null!;


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
