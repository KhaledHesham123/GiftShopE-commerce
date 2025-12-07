using IdentityService.Shared.Entites;
using System.Linq.Expressions;

namespace IdentityService.Shared.Repository
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAll();
        IQueryable<T> GetQueryableByCriteria(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> predicate);

        void SaveInclude(T entity);


        Task SaveChangesAsync();
    }
}
