using Ticketize.Application.Models.Mail;

namespace Ticketize.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email, CancellationToken cancellationToken = default);
    }
}
