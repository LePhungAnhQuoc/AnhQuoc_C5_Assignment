using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddReaderDto : IMap<Reader>
    {
        public string Id { get; set; } = null!;

        public string Lname { get; set; } = null!;

        public string Fname { get; set; } = null!;

        public DateTime BoF { get; set; }

        public bool ReaderType { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddReaderDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

        public void MapFrom(Reader entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Reader entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
