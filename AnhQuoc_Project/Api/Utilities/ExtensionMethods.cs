using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Api.Utilities
{
    public static class ExtensionMethods
    {
        public static bool Remove<TEntity>(this DbSet<TEntity> entities, string id) where TEntity : class
        {
            var entity = entities.Find(id);

            if (entity != null)
            {
                entities.Remove(entity);
                return true;
            }
            return false;
        }

        public static bool Remove<TEntity>(this DbSet<TEntity> entities, int id) where TEntity : class
        {
            var entity = entities.Find(id);

            if (entity != null)
            {
                entities.Remove(entity);
                return true;
            }
            return false;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> result = new ObservableCollection<T>(source);
            return result;
        }
    }
}
