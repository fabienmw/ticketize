using CsvHelper;
using System.Globalization;
using Ticketize.Application.Contracts.Infrastructure;
using Ticketize.Application.Features.Categories.Queries.GetEventsExport;

namespace Ticketize.Infrastructure.FileExport
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDto)
        {
            using var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(writer,CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(eventExportDto);
            }

            return memoryStream.ToArray();
        }
    }
}
