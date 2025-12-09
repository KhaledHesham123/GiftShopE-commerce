using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductCatalogService.Shared.Entities;
using System.Linq.Expressions;

namespace ProductCatalogService.Shared.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        IQueryable<T> GetQueryableByCriteria(Expression<Func<T, bool>> predicate);

        void SaveInclude(T entity, params string[] includedProperties);
        Task SaveChangesAsync();
    }
}

