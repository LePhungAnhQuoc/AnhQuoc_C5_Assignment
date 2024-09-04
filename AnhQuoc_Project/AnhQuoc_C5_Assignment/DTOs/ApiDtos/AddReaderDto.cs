using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddReaderDto : IMap<Reader>
    {
        public string Id { get; set; }

        public string LName { get; set; }

        public string FName { get; set; }

        public DateTime boF { get; set; }

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
