namespace Api.Models.Dtos
{
    public interface IMap<T> where T : class
    {
        public void MapFrom(T entity);
        public void MapTo(ref T entity);
    }
}
