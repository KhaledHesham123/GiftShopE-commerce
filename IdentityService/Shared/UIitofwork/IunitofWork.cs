namespace IdentityService.Shared.UIitofwork
{
    public interface IunitofWork: IDisposable
    {
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
