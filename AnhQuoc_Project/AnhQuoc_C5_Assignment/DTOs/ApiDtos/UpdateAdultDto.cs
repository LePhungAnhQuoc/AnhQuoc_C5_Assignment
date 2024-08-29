using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateAdultDto : IMap<Adult>
    {
        public string Identify { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public DateTime ExpireDate { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(Adult entity)
        {
            Utilities.Copy(this, entity);
        }

        public void MapTo(ref Adult entity)
        {
            Utilities.Copy(entity, this);
        }
    }
}
