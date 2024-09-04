using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateProvinceDto : IMap<Province>
    {
        public string Name { get; set; }

        public bool Status { get; set; }


        public void MapFrom(Province entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Province entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
