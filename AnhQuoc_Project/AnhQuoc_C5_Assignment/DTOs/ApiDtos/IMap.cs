namespace Api.Models.Dtos
{
    public interface IMap<T> where T : class
    {
        void MapFrom(T entity);
        void MapTo(ref T entity);
    }
}
