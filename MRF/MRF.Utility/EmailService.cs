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
        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent, string attachmentPath = null)
        {
            try
            {
                string[] emailTo = toEmail.Split(',');

                for (int i = 0; i < emailTo.Length; i++)
                {
                    var msg = new SendGridMessage
                    {
                        From = new EmailAddress(FromEmail, SenderName),
                        Subject = subject,
                        HtmlContent = htmlContent
                    };

                    msg.AddTo(emailTo[i]);

                    if (attachmentPath != null)
                    {
                        byte[] fileBytes = File.ReadAllBytes(attachmentPath);
                        msg.AddAttachment(Path.GetFileName(attachmentPath), Convert.ToBase64String(fileBytes));
                    }

                    var response = await _sendGridClient.SendEmailAsync(msg);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Exception occurred while sending email: {e.Message}");
                throw; // Re-throw the exception to maintain the flow
            }
        }


        public bool IsValidUpdateValue(object value)
        {
            return value != null
                && !(value is int intValue && intValue == 0)
                && !(value is string stringValue && stringValue.Equals("string"))
                && !string.IsNullOrEmpty(value.ToString())
                && !(value is DateOnly dateOnlyValue && dateOnlyValue.Year == 1)
                && !(value is bool boolValue && !boolValue);
        }
    }
}
