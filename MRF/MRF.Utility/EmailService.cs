using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MRF.Utility
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ISendGridClient _sendGridClient;
        private readonly string FromEmail;
        private readonly string SenderName;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            string sendGridApiKey = _configuration["SendGridSettings:ApiKey"];
            _sendGridClient = new SendGridClient(sendGridApiKey);
            FromEmail = _configuration["SendGridSettings:FromEmail"];
            SenderName = _configuration["SendGridSettings:SenderName"];
        }
        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress(FromEmail, SenderName),
                Subject = subject,
                HtmlContent = htmlContent                
            };

            msg.AddTo(toEmail);           
            
            var response = await _sendGridClient.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }
    }
}
