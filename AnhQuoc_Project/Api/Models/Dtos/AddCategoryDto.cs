using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddCategoryDto : IMap<Category>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

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
