using MediatR;

namespace Ticketize.Application.Features.Categories.Queries.GetEventsExport
{
    public class GetEventsExportQuery : IRequest<EventExportFileVm>
    {
    }
}
