using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddStatisticalDto : IMap<Statistical>
    {
        public DateTime DateTime { get; set; }

        public string Data { get; set; }

        public string Description { get; set; }


        public AddStatisticalDto()
        {
        }

        public void MapFrom(Statistical entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Statistical entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
