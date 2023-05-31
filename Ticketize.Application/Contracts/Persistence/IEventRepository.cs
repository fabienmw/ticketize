using Ticketize.Domain.Entities;

namespace Ticketize.Application.Contracts.Persistence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
    }
}
