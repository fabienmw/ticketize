namespace Ticketize.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(Task entity);
        Task UpdateAsync(Task entity);
        Task DeleteAsync(Task entity);
    }
}
