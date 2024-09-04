using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class AddFunctionDto : IMap<Function>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? IdParent { get; set; }

        public string? UrlImage { get; set; }

        public bool IsAdmin { get; set; }

        public bool Status { get; set; }


        public AddFunctionDto()
        {
            Status = true;
        }

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
