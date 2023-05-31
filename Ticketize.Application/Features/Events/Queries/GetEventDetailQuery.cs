using MediatR;

namespace Ticketize.Application.Features.Events.Queries
{
    public class GetEventDetailQuery : IRequest<EventDetailVm>
    {
        public Guid Id { get; set; }
    }
}
