using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Data.DBContexts;

namespace OrderService.Shared.UIitofwork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IDbContextTransaction? _Transaction { get; set; }

        public UnitofWork(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

     
        public void Dispose()
        {
            _Transaction?.Dispose();
            // Don't dispose DbContext - it's managed by DI container
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync();

        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _Transaction = await _dbContext.Database.BeginTransactionAsync();

        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_Transaction != null)
            {
                await _Transaction.CommitAsync();
                await _Transaction.DisposeAsync();
                _Transaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_Transaction != null)
            {
                await _Transaction.RollbackAsync();
                await _Transaction.DisposeAsync();
                _Transaction = null;
            }
        }
    }
}
