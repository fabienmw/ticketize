using MediatR;

namespace Ticketize.Application.Features.Events
{
    public class GetEventsListQuery : IRequest<List<EventListVm>>
    {

    }
}
