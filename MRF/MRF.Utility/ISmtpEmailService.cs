namespace MRF.Utility
{
    public interface ISmtpEmailService
    {
        void SendEmail(string receiverEmail, string subject, string body, string? attachmentPath = null);
    }
}
