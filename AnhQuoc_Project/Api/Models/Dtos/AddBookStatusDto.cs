using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddBookStatusDto : IMap<BookStatus>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddBookStatusDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

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
