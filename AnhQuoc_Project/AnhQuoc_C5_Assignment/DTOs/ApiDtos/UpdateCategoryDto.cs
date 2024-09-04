using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class UpdateCategoryDto : IMap<Category>
    {
        public string Name { get; set; }

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(Category entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Category entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
