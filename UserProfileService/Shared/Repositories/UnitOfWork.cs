using ProductCatalogService.Shared.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using ProductCatalogService.Data.DBContexts;

namespace ProductCatalogService.Shared.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserProfileDbContext _context;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(UserProfileDbContext context)
        {
            _context = context;

        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction is not null)
                return;
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction is null)
                return;

            await _currentTransaction.CommitAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction is null)
                return;

            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public void Dispose()
        {
            //_currentTransaction?.Dispose();
            //_context.Dispose();
        }
    }
}

