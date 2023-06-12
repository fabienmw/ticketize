using AutoMapper;
using MediatR;
using Ticketize.Application.Contracts.Persistence;
using Ticketize.Domain.Entities;

namespace Ticketize.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Event>(request);

            var validator = new CreateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            @event = await _eventRepository.AddAsync(@event);

            return @event.EventId;
        }
    }
}
