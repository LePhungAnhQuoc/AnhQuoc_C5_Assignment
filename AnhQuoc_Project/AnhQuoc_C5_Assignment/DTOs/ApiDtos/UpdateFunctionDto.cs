using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateFunctionDto : IMap<Function>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string IdParent { get; set; }

        public string UrlImage { get; set; }

        public bool IsAdmin { get; set; }

        public bool Status { get; set; }


        public void MapFrom(Function entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Function entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
