using AnhQuoc_C5_Assignment;
using System;

namespace Api.Models.Dtos
{
    public class AddCategoryDto : IMap<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }


        public AddCategoryDto()
        {
            Status = true;
            CreatedAt = ModifiedAt = DateTime.Now;
        }

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
