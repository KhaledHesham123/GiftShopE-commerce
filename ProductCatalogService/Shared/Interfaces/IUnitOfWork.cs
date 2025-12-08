namespace ProductCatalogService.Shared.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {   
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

