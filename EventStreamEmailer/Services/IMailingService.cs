namespace EventStreamEmailer.Services
{
    public interface IMailingService
    {
        public void SendEmail(string to, string subject, string body, bool isBodyHtml = false);
    }
}