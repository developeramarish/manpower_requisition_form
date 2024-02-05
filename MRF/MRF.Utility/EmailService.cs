using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using NLog;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System.Net.Mail;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace MRF.Utility
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly string senderEmail;
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly SmtpClient smtpClient;
        private readonly ISendGridClient _sendGridClient;
        private readonly string sendGridFromEmail;
        private readonly string sendGridSenderName;
        private readonly IUnitOfWork _unitOfWork;
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _logger = logger;
            _unitOfWork=unitOfWork;
            // Initialize SMTP settings
            senderEmail = _configuration["SMTP:senderEmail"];
            smtpServer = _configuration["SMTP:Server"];
            smtpPort = Convert.ToInt32(_configuration["SMTP:Port"]);
            smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.UseDefaultCredentials = false;

            // Initialize SendGrid settings
            string sendGridApiKey = _configuration["SendGridSettings:ApiKey"];
            _sendGridClient = new SendGridClient(sendGridApiKey);
            sendGridFromEmail = _configuration["SendGridSettings:FromEmail"];
            sendGridSenderName = _configuration["SendGridSettings:SenderName"];
        }
        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent, string attachmentPath = null)
        {
            try
            {
                if (IsSendGridEnabled())
                {
                    await SendEmailSendGrid(toEmail, subject, htmlContent, attachmentPath);
                }
                else
                {
                    SendEmailSMTP(toEmail, subject, htmlContent, attachmentPath);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred while sending email: {e.Message}");
                throw;
            }
        }

        public async Task SendEmailAsync(int senderId, string subject, string htmlContent, int mrfId)
        {
            try
            {
                string mrfRefNo = getMRFRefNoFromMRFId(mrfId);
                htmlContent = htmlContent.Replace("MRF#", $"<span style='color:red; font-weight:bold;'>MRF Id {mrfRefNo}</span>");
                if (IsSendGridEnabled())
                {
                    await SendEmailSendGrid(getUserEmail(senderId), subject, htmlContent);
                }
                else
                {
                    SendEmailSMTP(getUserEmail(senderId), subject, htmlContent);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred while sending email: {e.Message}");
                throw;
            }
        }

        private async Task SendEmailSendGrid(string toEmail, string subject, string htmlContent, string? attachmentPath=null)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress(sendGridFromEmail, sendGridSenderName),
                Subject = subject,
                HtmlContent = htmlContent
            };

            msg.AddTo(toEmail);

            if (!string.IsNullOrEmpty(attachmentPath))
            {
                byte[] fileBytes = File.ReadAllBytes(attachmentPath);
                msg.AddAttachment(Path.GetFileName(attachmentPath), Convert.ToBase64String(fileBytes));
            }

            var response = await _sendGridClient.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send email using SendGrid. Status code: {response.StatusCode}");
            }
        }

        private void SendEmailSMTP(string receiverEmail, string subject, string body, string? attachmentPath = null)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage(senderEmail, receiverEmail, subject, body))
                {
                    mailMessage.IsBodyHtml = true;
                    if (!string.IsNullOrEmpty(attachmentPath))
                    {
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
                        mailMessage.Attachments.Add(attachment);
                    }
                    smtpClient.Send(mailMessage);
                    _logger.LogInformation("Email sent successfully using SMTP.");
                }
            }
            catch (SmtpException ex)
            {
                _logger.LogError($"SMTP ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"EMAIL ERROR: {ex.Message}");
            }
        }

        private bool IsSendGridEnabled()
        {   
            return bool.Parse(_configuration["FeatureToggles:UseSendGrid"]);
        }

        public bool IsValidUpdateValue(object value)
        {
            return value != null
       && !(value is int intValue && intValue == 0)
       && !(value is string stringValue && stringValue.Equals("string"))
       && !string.IsNullOrEmpty(value.ToString())
       && !((value is DateTime dateTimeValue && dateTimeValue == DateTime.MinValue)
           || (value is DateOnly dateOnlyValue && dateOnlyValue == DateOnly.MinValue))
       && !(value is bool boolValue && !boolValue);
        }
        
        private string getMRFRefNoFromMRFId(int id)
        {
            Mrfdetails mrfdetails = _unitOfWork.Mrfdetail.Get(u => u.Id == id);

            if (mrfdetails != null)
                return mrfdetails.ReferenceNo;
            return string.Empty;
        }

        private string getUserEmail(int id)
        {
            Employeedetails Employeedetail = _unitOfWork.Employeedetails.Get(u => u.Id == id);

            if (Employeedetail != null)
                return Employeedetail.Email;
            return string.Empty;
        }
    }
}
