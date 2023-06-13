using AutoMapper;
using MediatR;
using Ticketize.Application.Contracts.Infrastructure;
using Ticketize.Application.Contracts.Persistence;
using Ticketize.Domain.Entities;

namespace Ticketize.Application.Features.Categories.Queries.GetEventsExport
{
    public class GetEvetsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;
        public GetEvetsExportQueryHandler(IAsyncRepository<Event> eventRepository, IMapper mapper, ICsvExporter csvExporter)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _csvExporter = csvExporter;
        }

        public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var allEvents = _mapper.Map<List<EventExportDto>>((await _eventRepository.ListAllAsync()).OrderBy(e => e.Date));

            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new EventExportFileVm
            {
                ContentType = "text/csv",
                Data = fileData,
                EventExportFileName = $"{Guid.NewGuid()}.csv"
            };

            return eventExportFileDto;
        }
    }
}
