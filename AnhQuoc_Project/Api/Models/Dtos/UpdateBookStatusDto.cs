using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateBookStatusDto : IMap<BookStatus>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(BookStatus entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref BookStatus entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
