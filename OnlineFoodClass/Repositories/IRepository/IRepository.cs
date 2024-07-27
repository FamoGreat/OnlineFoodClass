using System.Linq.Expressions;

namespace OnlineFoodClass.Repositories.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = null);
        void AddAsync(T entity);
        void RemoveAsync(T entity);
    }
}
