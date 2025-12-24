namespace OrderService.Shared.Entites
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public bool IsDeleted { get; set; }
    }
}
