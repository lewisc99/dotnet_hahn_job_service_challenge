namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
    }
}
