using Ticketize.Application.Contracts.Persistence;
using Ticketize.Domain.Entities;

namespace Ticketize.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(TicketizeDbContext ticketizeDbContext) : base(ticketizeDbContext)
        {
        }

        public Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate)
        {
            var matches = _ticketizeDbContext.Events.Any(e => e.Equals(name) && e.Date.Date.Equals(eventDate));

            return Task.FromResult(matches);
        }
    }
}
