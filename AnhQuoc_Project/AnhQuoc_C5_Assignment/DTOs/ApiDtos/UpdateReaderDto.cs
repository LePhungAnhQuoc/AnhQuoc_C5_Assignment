using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateReaderDto : IMap<Reader>
    {
        public string LName { get; set; }

        public string FName { get; set; }

        public DateTime boF { get; set; }

        public bool ReaderType { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


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
