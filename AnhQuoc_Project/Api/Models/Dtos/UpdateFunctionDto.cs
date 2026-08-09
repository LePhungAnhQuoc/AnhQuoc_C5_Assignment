using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateFunctionDto : IMap<UserFunction>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? IdParent { get; set; }

        public string? UrlImage { get; set; }

        public bool IsAdmin { get; set; }

        public bool Status { get; set; }


        public void MapFrom(UserFunction entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref UserFunction entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
