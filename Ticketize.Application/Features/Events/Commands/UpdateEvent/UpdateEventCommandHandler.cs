using AutoMapper;
using MediatR;
using Ticketize.Application.Contracts.Persistence;
using Ticketize.Application.Exceptions;
using Ticketize.Domain.Entities;

namespace Ticketize.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        public UpdateEventCommandHandler(IAsyncRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId) ?? throw new NotFoundException(nameof(Event), request.EventId);

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

            await _eventRepository.UpdateAsync(eventToUpdate);
        }
    }
}
