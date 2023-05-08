using System.Net.Mail;

namespace EventStreamEmailer.Services
{
    public class MailingService : IMailingService
    {
        private readonly SmtpClient _smtpClient;
        private readonly ILogger<MailingService> _logger;

        public MailingService(ILogger<MailingService> logger, SmtpClient smtpClient)
        {
            _logger = logger;
            _smtpClient = smtpClient;
        }

        public void SendEmail(string to, string subject, string body, bool isBodyHtml = false)
        {
            _logger.LogInformation("Sending email to {to}", to);

            MailMessage message = new("noreply@dhl.non-official.com", to) {
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
            };

            System.Console.WriteLine("Sending email to {0}", to);

            _smtpClient.Send(message);
            message.Dispose();
        }
    }
}