using Microsoft.EntityFrameworkCore;
using OrderService.Data.DBContexts;
using OrderService.Shared.Entites;
using OrderService.Shared.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace IdentityService.Shared.Repository
{
    public class GenericRepository<T>: IGenericRepository<T>where T : BaseEntity
    {

        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext applicationDbcontext;

        public GenericRepository(ApplicationDbContext applicationDbcontext)
        {
            _dbSet = applicationDbcontext.Set<T>();
            this.applicationDbcontext = applicationDbcontext;
        }


        public IQueryable<T> GetAll()
        {
            return applicationDbcontext.Set<T>().Where(e => e.IsDeleted == false).AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllByCriteriaAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetQueryableByCriteria(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }


        public IQueryable<T> GetQueryableByCriteriaAndInclude(Expression<Func<T, bool>> predicate , params Expression<Func<T, object>>[] includeExpressions) 
        {
            var query = _dbSet.AsNoTracking().Where(predicate);
            foreach (var includeExpression in includeExpressions)
            {
                query = query.Include(includeExpression);
            }
            return query;
        }



        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }


        public void  Delete(T entity)
        {
            _dbSet.Remove(entity);

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await applicationDbcontext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public void SaveInclude(T entity)
        {
            var existingEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                existingEntity = _dbSet.FirstOrDefault(e => e.Id == entity.Id);
                if (existingEntity == null)
                    throw new Exception($"Entity of type {typeof(T).Name} with Id {entity.Id} not found.");

                _dbSet.Attach(existingEntity);

            }

            var entry = applicationDbcontext.Entry(existingEntity);
            var keyNames = entry.Metadata.FindPrimaryKey().Properties.Select(p => p.Name).ToList();


            foreach (var prop in typeof(T).GetProperties())
            {
                if (entry.Metadata.FindProperty(prop.Name) == null)
                    continue;

                if (keyNames.Contains(prop.Name))
                    continue;

                var oldvalue = prop.GetValue(existingEntity);
                var newvale = prop.GetValue(entity);

                if (newvale != null && !object.Equals(oldvalue, newvale))
                {
                    prop.SetValue(existingEntity, newvale);
                    entry.Property(prop.Name).IsModified = true;
                }
                else
                {
                    entry.Property(prop.Name).IsModified = false;
                }


            }
        }
    }
}
