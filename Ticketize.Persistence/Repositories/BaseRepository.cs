using Microsoft.EntityFrameworkCore;
using Ticketize.Application.Contracts.Persistence;

namespace Ticketize.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T>
        where T : class
    {
        protected readonly TicketizeDbContext _ticketizeDbContext;

        public BaseRepository(TicketizeDbContext ticketizeDbContext)
        {
            _ticketizeDbContext = ticketizeDbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _ticketizeDbContext.Set<T>().AddAsync(entity);
            await _ticketizeDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _ticketizeDbContext.Set<T>().Remove(entity);
            await _ticketizeDbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return (await _ticketizeDbContext.Set<T>().FindAsync(id))!;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _ticketizeDbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _ticketizeDbContext.Entry(entity).State = EntityState.Modified;
            await _ticketizeDbContext.SaveChangesAsync();
        }
    }
}
