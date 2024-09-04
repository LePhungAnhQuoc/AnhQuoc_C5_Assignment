using AnhQuoc_C5_Assignment;

namespace Api.Models.Dtos
{
    public class AddProvinceDto : IMap<Province>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }


        public AddProvinceDto()
        {
            Status = true;
        }

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
