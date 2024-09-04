using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateBookTitleDto : IMap<BookTitle>
    {
        public string IdCategory { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Note { get; set; }

        public string Summary { get; set; } = null!;

        public string UrlImage { get; set; } = null!;


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
