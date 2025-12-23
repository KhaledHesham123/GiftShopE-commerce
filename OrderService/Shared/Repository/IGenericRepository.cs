using OrderService.Shared.Entites;
using System.Linq.Expressions;

namespace OrderService.Shared.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAll();
        IQueryable<T> GetQueryableByCriteria(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> predicate);
        void SaveInclude(T entity);
        Task SaveChangesAsync();
        IQueryable<T> GetQueryableByCriteriaAndInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
    }
}
