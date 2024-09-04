using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddAdultDto : IMap<Adult>
    {
        public string IdReader { get; set; }

        public string Identify { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public DateTime ExpireDate { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public AddAdultDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

        public void MapFrom(Adult entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Adult entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
