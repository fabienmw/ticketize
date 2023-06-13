using Ticketize.Application.Features.Categories.Queries.GetEventsExport;

namespace Ticketize.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDto);
    }
}
