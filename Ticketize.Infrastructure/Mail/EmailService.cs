using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using Ticketize.Application.Contracts.Infrastructure;
using Ticketize.Application.Models.Mail;

namespace Ticketize.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<bool> SendEmail(Email email, CancellationToken cancellationToken = default)
        {
            var client = new SendGridClient(_settings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;
            var from = new EmailAddress
            {
                Email = _settings.FromAddress,
                Name = _settings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage, cancellationToken);

            return (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted);
        }
    }
}
