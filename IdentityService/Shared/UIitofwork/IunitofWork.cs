namespace IdentityService.Shared.UIitofwork
{
    public interface IUnitofWork: IDisposable
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
