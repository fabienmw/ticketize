using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ticketize.Application.Contracts.Infrastructure;
using Ticketize.Application.Contracts.Persistence;
using Ticketize.Application.Exceptions;
using Ticketize.Application.Models.Mail;
using Ticketize.Domain.Entities;

namespace Ticketize.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;
        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService, ILogger<CreateEventCommandHandler> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Event>(request);

            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any()) 
                throw new ValidationException(validationResult);

            @event = await _eventRepository.AddAsync(@event);

            // TODO: Use email template to format the mails
            var email = new Email { To = "fabien@stacktech.co.za", Body = $"A new event was created: {request}", Subject = "A new event was created" };

            try
            {
                await _emailService.SendEmail(email);    
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email for event: [{@event.EventId}], error details: {ex.Message}");
            }

            return @event.EventId;
        }
    }
}
